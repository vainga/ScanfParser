using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ScanfParser.ParserLogic;

namespace ScanfParser.RegExParser
{
    public enum ErrorType
    {
        Insert,
        Replace,
        Delete,
        Expected
    }

    public class ParseError
    {
        public ErrorType Type { get; }
        public string Message { get; }
        public int Position { get; }

        public ParseError(ErrorType type, string message, int position)
        {
            Type = type;
            Message = message;
            Position = position;
        }

        public override string ToString()
        {
            return $"[{Type}] Позиция {Position}: {Message}";
        }
    }

    public class Correction
    {
        public int Position { get; }
        public string ToInsert { get; }
        public string ToReplace { get; }
        public int DeleteLength { get; }
        public ErrorType Type { get; }

        public Correction(int position, string toInsert, ErrorType type)
        {
            Position = position;
            ToInsert = toInsert;
            Type = type;
            DeleteLength = 0;
        }

        public Correction(int position, string toReplace, string toInsert, ErrorType type)
        {
            Position = position;
            ToReplace = toReplace;
            ToInsert = toInsert;
            Type = type;
            DeleteLength = toReplace?.Length ?? 0;
        }

        public Correction(int position, int deleteLength, ErrorType type)
        {
            Position = position;
            DeleteLength = deleteLength;
            Type = type;
            ToInsert = "";
        }
    }

    public class Parser
    {
        private readonly LexemeBuffer _buffer;
        private readonly List<ParseError> _errors;
        private readonly List<Correction> _corrections;
        private readonly List<Lexeme> _originalLexemes;
        private string _originalInput;

        public Parser(List<Lexeme> lexemes)
        {
            _buffer = new LexemeBuffer(lexemes);
            _errors = new List<ParseError>();
            _corrections = new List<Correction>();
            _originalLexemes = new List<Lexeme>(lexemes);
        }

        public void SetOriginalInput(string input)
        {
            _originalInput = input;
        }

        public List<string> Parse()
        {
            _errors.Clear();
            _corrections.Clear();
            bool hasSuccessfulScanf = false;

            while (_buffer.Current.type != LexemeType.EOF)
            {
                if (_buffer.Current.type == LexemeType.UNKNOWN)
                {
                    AddError(ErrorType.Delete, $"Лексическая ошибка: неизвестный символ '{_buffer.Current.value}'", _buffer.Position);
                    AddCorrection(new Correction(GetCharPosition(_buffer.Position), _buffer.Current.value.Length, ErrorType.Delete));
                    _buffer.Next();
                    continue;
                }

                int errorCountBefore = _errors.Count;

                if (_buffer.Current.type != LexemeType.SCANFCALL)
                {
                    AddError(ErrorType.Expected, "Ожидается вызов 'scanf'", _buffer.Position);
                    BC();
                    Expect(LexemeType.SEMICOLON, "Ожидается ';' после вызова scanf", ErrorType.Expected);
                }
                else
                {
                    SC();
                }

                if (_errors.Count == errorCountBefore)
                {
                    hasSuccessfulScanf = true;
                }
            }

            List<string> results = new();

            foreach (var error in _errors)
            {
                results.Add(error.ToString());
            }

            // Добавляем исправленную строку в конец
            if (_errors.Count > 0)
            {
                string correctedString = GenerateCorrectedString();
                results.Add($"Исправленная строка: {correctedString}");
            }

            return results;
        }

        // <SC> -> 'scanf' <BC> ';'
        private void SC()
        {
            if (_buffer.Current.type != LexemeType.SCANFCALL)
            {
                AddError(ErrorType.Expected, "Ожидается вызов 'scanf'", _buffer.Position);
                SkipToNextStatement();
                return;
            }

            Expect(LexemeType.SCANFCALL, "Ожидается вызов 'scanf'", ErrorType.Expected);
            BC();
            Expect(LexemeType.SEMICOLON, "Ожидается ';' после вызова scanf", ErrorType.Expected);
        }

        // <BC> -> '(' <FS> <A> ')'
        private void BC()
        {
            Expect(LexemeType.L_BRACKET, "Ожидается '(' после 'scanf'", ErrorType.Expected);

            int formatSpecifiersCount = FS();

            // Пропускаем пробелы после форматной строки
            while (_buffer.Current.type == LexemeType.WHITESPACE)
                _buffer.Next();

            A(formatSpecifiersCount);

            Expect(LexemeType.R_BRACKET, "Ожидается ')' в конце вызова scanf", ErrorType.Expected);
        }

        // <FS> -> '"'<FC>'"'
        private int FS()
        {
            if (_buffer.Current.type != LexemeType.FORMAT_STRING)
            {
                AddError(ErrorType.Expected, "Ожидается строка формата (например, \"%d\")", _buffer.Position);
                AddCorrection(new Correction(GetCharPosition(_buffer.Position), "\"%d\"", ErrorType.Insert));
                return 1; // Предполагаем один спецификатор по умолчанию
            }

            string formatString = _buffer.Current.value;
            int count = CountFormatSpecifiers(formatString);
            _buffer.Next();
            return count;
        }

        // <A> -> ε | ',' '&'<N><MA>
        private void A(int expectedCount)
        {
            int actualCount = 0;
            List<string> variableNames = new List<string>();

            while (_buffer.Current.type == LexemeType.WHITESPACE)
                _buffer.Next();

            while (_buffer.Current.type != LexemeType.R_BRACKET &&
                   _buffer.Current.type != LexemeType.SEMICOLON &&
                   _buffer.Current.type != LexemeType.EOF)
            {
                while (_buffer.Current.type == LexemeType.WHITESPACE)
                    _buffer.Next();

                if (_buffer.Current.type == LexemeType.COMMA)
                {
                    int commaPosition = _buffer.Position;
                    _buffer.Next();

                    if (_buffer.Current.type == LexemeType.R_BRACKET || _buffer.Current.type == LexemeType.SEMICOLON)
                    {
                        if (actualCount < expectedCount)
                        {
                            AddError(ErrorType.Insert, "Ожидается переменная после запятой", _buffer.Position);
                            string varName = GenerateVariableName(actualCount);
                            AddCorrection(new Correction(GetCharPosition(_buffer.Position), $"&{varName}", ErrorType.Insert));
                            actualCount++;
                        }
                        else
                        {
                            AddError(ErrorType.Delete, "Удалить лишнюю запятую", commaPosition);
                            AddCorrection(new Correction(GetCharPosition(commaPosition), 1, ErrorType.Delete));
                        }
                        continue;
                    }

                    if (_buffer.Current.type == LexemeType.AMPERSAND)
                    {
                        _buffer.Next();
                        string varName = N();
                        if (!string.IsNullOrEmpty(varName))
                        {
                            actualCount++;
                            variableNames.Add(varName);
                        }
                    }
                    else
                    {
                        AddError(ErrorType.Expected, "Ожидается '&' после запятой", _buffer.Position);
                        AddCorrection(new Correction(GetCharPosition(_buffer.Position), "&", ErrorType.Insert));
                        _buffer.Next();
                    }
                }
                else
                {
                    AddError(ErrorType.Delete, $"Удалить неожиданный символ: {_buffer.Current.value}", _buffer.Position);
                    AddCorrection(new Correction(GetCharPosition(_buffer.Position), _buffer.Current.value.Length, ErrorType.Delete));
                    _buffer.Next();
                }
            }

            // Добавляем недостающие переменные
            if (expectedCount > actualCount)
            {
                AddError(ErrorType.Insert, $"Ожидается {expectedCount} переменных", _buffer.Position);
                
                for (int i = actualCount; i < expectedCount; i++)
                {
                    string varName = GenerateVariableName(i);
                    string toInsert = i == 0 ? $", &{varName}" : $", &{varName}";
                    if (actualCount == 0 && i == 0)
                        toInsert = $", &{varName}";
                    
                    AddCorrection(new Correction(GetCharPosition(_buffer.Position), toInsert, ErrorType.Insert));
                }
            }
            else if (actualCount > expectedCount)
            {
                AddError(ErrorType.Delete, $"Удалить {actualCount - expectedCount} лишнюю(ие) переменную(ые)", _buffer.Position);
            }
        }

        // <N> -> <L><NT>
        private string N()
        {
            if (_buffer.Current.type != LexemeType.VARIABLE)
            {
                AddError(ErrorType.Insert, "Ожидается имя переменной после '&'", _buffer.Position);
                string varName = GenerateVariableName(0);
                AddCorrection(new Correction(GetCharPosition(_buffer.Position), varName, ErrorType.Insert));
                return varName;
            }

            string variable = _buffer.Current.value;
            _buffer.Next();
            return variable;
        }

        private void Expect(LexemeType expectedType, string errorMessage, ErrorType type)
        {
            if (_buffer.Current.type == expectedType)
            {
                _buffer.Next();
            }
            else
            {
                AddError(type, errorMessage, _buffer.Position);
                string defaultValue = GetDefaultValue(expectedType);
                AddCorrection(new Correction(GetCharPosition(_buffer.Position), defaultValue, ErrorType.Insert));
            }
        }

        private string GetDefaultValue(LexemeType type)
        {
            return type switch
            {
                LexemeType.L_BRACKET => "(",
                LexemeType.R_BRACKET => ")",
                LexemeType.SEMICOLON => ";",
                LexemeType.FORMAT_STRING => "\"%d\"",
                LexemeType.AMPERSAND => "&",
                LexemeType.COMMA => ",",
                LexemeType.SCANFCALL => "scanf",
                _ => ""
            };
        }

        private string GenerateVariableName(int index)
        {
            char letter = (char)('x' + (index % 3)); // x, y, z
            return letter.ToString();
        }

        private void AddCorrection(Correction correction)
        {
            _corrections.Add(correction);
        }

        private void AddError(ErrorType type, string message, int lexemePosition)
        {
            int charPosition = GetCharPosition(lexemePosition);
            _errors.Add(new ParseError(type, message, charPosition));
        }

        private int GetCharPosition(int lexemePosition)
        {
            int charPosition = 0;
            for (int i = 0; i < lexemePosition && i < _originalLexemes.Count; i++)
            {
                charPosition += _originalLexemes[i].value.Length;
            }
            return charPosition;
        }

        private void SkipToClosingBracket()
        {
            while (_buffer.Current.type != LexemeType.R_BRACKET &&
                   _buffer.Current.type != LexemeType.SEMICOLON &&
                   _buffer.Current.type != LexemeType.EOF)
            {
                _buffer.Next();
            }
        }

        private void SkipToNextStatement()
        {
            while (_buffer.Current.type != LexemeType.SEMICOLON &&
                   _buffer.Current.type != LexemeType.EOF)
            {
                _buffer.Next();
            }

            if (_buffer.Current.type == LexemeType.SEMICOLON)
                _buffer.Next();
        }

        private int CountFormatSpecifiers(string formatString)
        {
            string cleanFormat = formatString.Trim('"');
            var matches = Regex.Matches(cleanFormat, @"%.");
            int valid = 0;

            foreach (Match match in matches)
            {
                string spec = match.Value;
                if (spec is "%d" or "%c" or "%f")
                {
                    valid++;
                }
                else
                {
                    _errors.Add(new ParseError(ErrorType.Replace,
                        $"Неверный спецификатор формата '{spec}', замените на %c, %d, %f",
                        GetFormatCharPosition(formatString, spec)));
                    
                    // Добавляем коррекцию для замены неверного спецификатора
                    int pos = GetFormatCharPosition(formatString, spec);
                    AddCorrection(new Correction(pos, spec, "%d", ErrorType.Replace));
                    valid++;
                }
            }

            return valid;
        }

        private int GetFormatCharPosition(string fullFormatString, string specifier)
        {
            int index = fullFormatString.IndexOf(specifier, StringComparison.Ordinal);
            return index >= 0 ? index : 0;
        }

        private string GenerateCorrectedString()
        {
            if (string.IsNullOrEmpty(_originalInput))
            {
                // Если нет оригинальной строки, создаем базовую структуру
                return BuildBasicScanfStructure();
            }

            string result = _originalInput;
            
            // Сортируем коррекции по позиции в обратном порядке, чтобы изменения не влияли на позиции
            var sortedCorrections = _corrections.OrderByDescending(c => c.Position).ToList();

            foreach (var correction in sortedCorrections)
            {
                switch (correction.Type)
                {
                    case ErrorType.Insert:
                        if (correction.Position <= result.Length)
                            result = result.Insert(correction.Position, correction.ToInsert);
                        else
                            result += correction.ToInsert;
                        break;
                        
                    case ErrorType.Replace:
                        if (correction.Position < result.Length)
                        {
                            int endPos = Math.Min(correction.Position + correction.DeleteLength, result.Length);
                            result = result.Remove(correction.Position, endPos - correction.Position);
                            result = result.Insert(correction.Position, correction.ToInsert);
                        }
                        break;
                        
                    case ErrorType.Delete:
                        if (correction.Position < result.Length)
                        {
                            int endPos = Math.Min(correction.Position + correction.DeleteLength, result.Length);
                            result = result.Remove(correction.Position, endPos - correction.Position);
                        }
                        break;
                }
            }

            return result;
        }

        private string BuildBasicScanfStructure()
        {
            // Построить базовую корректную структуру scanf
            int formatSpecifierCount = 1; // По умолчанию
            
            // Попытаться определить количество спецификаторов из ошибок
            foreach (var error in _errors)
            {
                if (error.Message.Contains("Ожидается") && error.Message.Contains("переменных"))
                {
                    var match = Regex.Match(error.Message, @"Ожидается (\d+) переменных");
                    if (match.Success && int.TryParse(match.Groups[1].Value, out int count))
                    {
                        formatSpecifierCount = count;
                        break;
                    }
                }
            }

            string formatString = "";
            string variables = "";
            
            for (int i = 0; i < formatSpecifierCount; i++)
            {
                formatString += "%d";
                if (i < formatSpecifierCount - 1)
                    formatString += " ";
                    
                if (i > 0)
                    variables += ", ";
                variables += "&" + GenerateVariableName(i);
            }

            return $"scanf(\"{formatString}\", {variables});";
        }
    }
}