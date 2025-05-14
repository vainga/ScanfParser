using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ScanfParser.ParserLogic;

namespace ScanfParser.RegExParser
{
    public enum ErrorType
    {
        Insert,    // Добавить
        Replace,   // Изменить
        Delete,    // Удалить
        Expected   // Ожидалось
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

    public class Parser
    {
        private readonly LexemeBuffer _buffer;
        private readonly List<ParseError> _errors;

        public Parser(List<Lexeme> lexemes)
        {
            _buffer = new LexemeBuffer(lexemes);
            _errors = new List<ParseError>();
        }

        public List<string> Parse()
        {
            _errors.Clear();
            bool foundScanf = false;

            while (_buffer.Current.type != LexemeType.EOF)
            {
                if (_buffer.Current.type == LexemeType.SCANFCALL)
                {
                    foundScanf = true;
                }

                ParseSingleStatement();
            }

            if (!foundScanf)
            {
                //_errors.Add(new ParseError(ErrorType.Expected, "Ожидается вызов 'scanf'", 0));
            }

            if (_errors.Count == 0)
            {
                return new List<string> { "Синтаксический анализ завершен успешно!" };
            }

            List<string> formatted = new();
            foreach (var error in _errors)
                formatted.Add(error.ToString());

            return formatted;
        }


        private void ParseSingleStatement()
        {
            int startPos = _buffer.Position;
            bool wasCommaWithoutVariable = false;

            if (_buffer.Current.type != LexemeType.SCANFCALL)
            {
                AddError(ErrorType.Expected, "Ожидается вызов 'scanf'", _buffer.Position);
                while (_buffer.Current.type != LexemeType.SEMICOLON &&
                       _buffer.Current.type != LexemeType.EOF)
                {
                    _buffer.Next();
                }

                if (_buffer.Current.type == LexemeType.SEMICOLON)
                    _buffer.Next();

                return;
            }

            Expect(LexemeType.SCANFCALL, "Ожидается вызов 'scanf'", ErrorType.Expected);
            Expect(LexemeType.L_BRACKET, "Ожидается '(' после 'scanf'", ErrorType.Expected);

            int formatSpecifiersCount = 0;
            if (_buffer.Current.type == LexemeType.FORMAT_STRING)
            {
                string formatString = _buffer.Current.value;
                formatSpecifiersCount = CountFormatSpecifiers(formatString);
                _buffer.Next();

                if (formatSpecifiersCount > 0 && _buffer.Current.type != LexemeType.COMMA &&
                _buffer.Current.type != LexemeType.R_BRACKET)
                {
                    AddError(ErrorType.Insert, "Добавить запятую после строки формата", _buffer.Position);
                    AddCorrection(LexemeType.COMMA, ",");
                }
            }
            else
            {
                AddError(ErrorType.Expected, "Ожидается строка формата (например, \"%d\")", _buffer.Position);
            }

            int variablesCount = 0;
            while (_buffer.Current.type != LexemeType.R_BRACKET &&
                   _buffer.Current.type != LexemeType.EOF)
            {
                if (_buffer.Current.type == LexemeType.COMMA)
                {
                    int commaPosition = _buffer.Position;
                    _buffer.Next();

                    if (_buffer.Current.type == LexemeType.R_BRACKET || _buffer.Current.type == LexemeType.SEMICOLON)
                    {
                        if (variablesCount < formatSpecifiersCount)
                        {
                            AddError(ErrorType.Insert, $"Добавить {formatSpecifiersCount - variablesCount} переменную(ые)", startPos);
                            wasCommaWithoutVariable = true;
                        }
                        else
                        {
                            AddError(ErrorType.Delete, "Удалить лишнюю запятую", commaPosition);
                        }
                        continue;
                    }
                }

                if (_buffer.Current.type == LexemeType.AMPERSAND)
                {
                    _buffer.Next();
                    if (_buffer.Current.type == LexemeType.VARIABLE)
                    {
                        variablesCount++;
                        _buffer.Next();
                    }
                    else
                    {
                        AddError(ErrorType.Insert, "Добавить имя переменной после '&'", _buffer.Position);
                    }
                }
                else if (_buffer.Current.type == LexemeType.VARIABLE)
                {
                    AddError(ErrorType.Insert, "Пропущен символ '&' перед переменной", _buffer.Position);
                    variablesCount++;
                    _buffer.Next();
                }
                else
                {
                    if (_buffer.Current.type == LexemeType.R_BRACKET || _buffer.Current.type == LexemeType.SEMICOLON)
                        break;

                    AddError(ErrorType.Delete, $"Удалить неожиданный символ: {_buffer.Current.value}", _buffer.Position);
                    _buffer.Next();
                }

            }

            Expect(LexemeType.R_BRACKET, "Ожидается ')' в конце вызова scanf", ErrorType.Expected);
            Expect(LexemeType.SEMICOLON, "Ожидается ';' после вызова scanf", ErrorType.Expected);

            if (!wasCommaWithoutVariable && formatSpecifiersCount > 0 && variablesCount != formatSpecifiersCount)
            {
                if (variablesCount < formatSpecifiersCount)
                {
                    AddError(ErrorType.Insert,
                        $"Добавить {formatSpecifiersCount - variablesCount} переменную(ые)", startPos);
                }
                else
                {
                    AddError(ErrorType.Delete,
                        $"Удалить {variablesCount - formatSpecifiersCount} лишнюю(ие) переменную(ые)", startPos);
                }
            }
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
                AddCorrection(expectedType, GetDefaultValue(expectedType));
                _buffer.Next();
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
                _ => ""
            };
        }

        private void AddCorrection(LexemeType expectedType, string defaultValue)
        {
            _buffer._lexemes.Insert(_buffer.Position, new Lexeme(expectedType, defaultValue));
        }

        private void AddError(ErrorType type, string message, int lexemePosition)
        {
            int charPosition = 0;
            for (int i = 0; i < lexemePosition && i < _buffer._lexemes.Count; i++)
            {
                charPosition += _buffer._lexemes[i].value.Length;
            }

            _errors.Add(new ParseError(type, message, charPosition));
        }

        private int GetFormatCharPosition(string fullFormatString, string specifier)
        {
            int index = fullFormatString.IndexOf(specifier, StringComparison.Ordinal);
            return index >= 0 ? index : 0;
        }


        private int CountFormatSpecifiers(string formatString)
        {
            string cleanFormat = formatString.Trim('"');

            var allSpecifiers = Regex.Matches(cleanFormat, @"%[a-zA-Z]");

            int validCount = 0;
            foreach (Match match in allSpecifiers)
            {
                string spec = match.Value;

                if (spec is "%d" or "%c" or "%f")
                {
                    validCount++;
                }
                else
                {
                    _errors.Add(new ParseError(
                        ErrorType.Replace,
                        $"Неверный спецификатор формата '{spec}', замените на %c, %d, %f",
                        GetFormatCharPosition(formatString, spec)));
                }
            }

            return validCount;
        }

    }
}