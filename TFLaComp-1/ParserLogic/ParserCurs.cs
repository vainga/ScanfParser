using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TFLaComp_1.ParserLogic
{
    public enum LexemeType
    {
        SCANFCALL,          // вызов scanf
        L_BRACKET,          // '('
        R_BRACKET,          // ')'
        FORMAT_STRING,      // строка формата, например, "%d %f"
        AMPERSAND,          // '&' (для взятия адреса переменной)
        VARIABLE,           // имя переменной (например, x, value)
        COMMA,              // ',' (разделитель аргументов)
        MARKS,              // кавычки (для строки формата)
        SEMICOLON,          // ';' (конец инструкции)
        WHITESPACE,
        EOF                 // конец входных данных
    }

    public class Lexeme
    {
        public LexemeType type;
        public string value;

        public Lexeme(LexemeType type, string value)
        {
            this.type = type;
            this.value = value;
        }
    }


    public class ParserCurs
    {
        private static readonly Dictionary<LexemeType, Regex> LexemePatterns = new Dictionary<LexemeType, Regex>
        {
            { LexemeType.SCANFCALL, new Regex(@"^scanf\b") },
            { LexemeType.L_BRACKET, new Regex(@"^\(") },
            { LexemeType.R_BRACKET, new Regex(@"^\)") },
            { LexemeType.FORMAT_STRING, new Regex(@"^""([^""\\]*(\\.[^""\\]*)*)""") }, // строка в кавычках
            { LexemeType.AMPERSAND, new Regex(@"^&") },
            { LexemeType.VARIABLE, new Regex(@"^[a-zA-Z_][a-zA-Z0-9_]*") }, // переменная (без &)
            { LexemeType.COMMA, new Regex(@"^,") },
            { LexemeType.MARKS, new Regex(@"^""") },
            { LexemeType.SEMICOLON, new Regex(@"^;") },
            { LexemeType.WHITESPACE, new Regex(@"^\s+") }

        };

        public static List<Lexeme> LexAnalyze(string expText)
        {
            List<Lexeme> lexemes = new List<Lexeme>();
            int pos = 0;

            while (pos < expText.Length)
            {
                bool matchFound = false;
                string remainingText = expText.Substring(pos);

                foreach (var pattern in LexemePatterns)
                {
                    Match match = pattern.Value.Match(remainingText);
                    if (match.Success)
                    {
                        string matchedValue = match.Value;
                        lexemes.Add(new Lexeme(pattern.Key, matchedValue));
                        pos += matchedValue.Length;
                        matchFound = true;
                        break;
                    }
                }

                if (!matchFound)
                {
                    if (char.IsWhiteSpace(expText[pos]))
                    {
                        pos++;
                        continue;
                    }
                    throw new Exception($"Unexpected character at position {pos}: '{expText[pos]}'");
                }
            }

            lexemes.Add(new Lexeme(LexemeType.EOF, ""));
            return lexemes;
        }
    }

    public class LexemeBuffer
    {
        private readonly List<Lexeme> _lexemes;
        private int _position;

        public LexemeBuffer(List<Lexeme> lexemes)
        {
            _lexemes = lexemes;
            _position = 0;
        }

        public Lexeme Next()
        {
            if (_position >= _lexemes.Count)
                return new Lexeme(LexemeType.EOF, "");

            return _lexemes[_position++];
        }

        public void Back()
        {
            if (_position > 0)
                _position--;
        }

        public Lexeme Current
        {
            get
            {
                if (_position >= _lexemes.Count)
                    return new Lexeme(LexemeType.EOF, "");

                return _lexemes[_position];
            }
        }

        public int Position => _position;

        public int Count => _lexemes.Count;
    }

    public class Parser
    {
        private readonly LexemeBuffer _buffer;
        private readonly List<string> _errors;

        public Parser(List<Lexeme> lexemes)
        {
            _buffer = new LexemeBuffer(lexemes);
            _errors = new List<string>();
        }

        public List<string> Parse()
        {
            _errors.Clear();

            while (_buffer.Current.type != LexemeType.EOF)
            {
                ParseSingleStatement();
            }

            if (_errors.Count == 0)
            {
                _errors.Add("Синтаксический анализ завершен успешно! 🎉");
            }

            return _errors;
        }

        private void ParseSingleStatement()
        {
            // Reset position to start of statement for error collection
            int startPos = _buffer.Position;

            // 1. Check for 'scanf'
            if (_buffer.Current.type != LexemeType.SCANFCALL)
            {
                _errors.Add("Ожидается вызов 'scanf'");
                SkipToNextStatement();
                return;
            }

            _buffer.Next();

            // 2. Check for '('
            if (_buffer.Current.type != LexemeType.L_BRACKET)
            {
                _errors.Add("Ожидается '(' после 'scanf'");
            }
            else
            {
                _buffer.Next();
            }

            // 3. Check for format string
            if (_buffer.Current.type != LexemeType.FORMAT_STRING)
            {
                _errors.Add("Ожидается строка формата (например, \"%d\")");
            }
            else
            {
                _buffer.Next();
            }

            // 4. Process arguments
            while (_buffer.Current.type != LexemeType.R_BRACKET &&
                   _buffer.Current.type != LexemeType.EOF)
            {
                if (_buffer.Current.type == LexemeType.COMMA)
                {
                    _buffer.Next();

                    // After comma, expect either &variable or another argument
                    if (_buffer.Current.type == LexemeType.AMPERSAND)
                    {
                        _buffer.Next();

                        if (_buffer.Current.type != LexemeType.VARIABLE)
                        {
                            _errors.Add("Ожидается имя переменной после '&'");
                        }
                        else
                        {
                            _buffer.Next();
                        }
                    }
                    else
                    {
                        _errors.Add("Ожидается '&' перед именем переменной");
                    }
                }
                else if (_buffer.Current.type == LexemeType.AMPERSAND)
                {
                    _buffer.Next();

                    if (_buffer.Current.type != LexemeType.VARIABLE)
                    {
                        _errors.Add("Ожидается имя переменной после '&'");
                    }
                    else
                    {
                        _buffer.Next();
                    }
                }
                else if (_buffer.Current.type != LexemeType.R_BRACKET)
                {
                    _errors.Add($"Неожиданный символ: {_buffer.Current.value}");
                    _buffer.Next();
                }
            }

            // 5. Check for closing bracket
            if (_buffer.Current.type != LexemeType.R_BRACKET)
            {
                _errors.Add("Ожидается ')' в конце вызова scanf");
            }
            else
            {
                _buffer.Next();
            }

            // 6. Check for semicolon
            if (_buffer.Current.type != LexemeType.SEMICOLON)
            {
                _errors.Add("Ожидается ';' после вызова scanf");
            }
            else
            {
                _buffer.Next();
            }

            // Skip whitespace between statements
            while (_buffer.Current.type == LexemeType.WHITESPACE)
            {
                _buffer.Next();
            }
        }

        private void SkipToNextStatement()
        {
            // Skip until we find a semicolon or EOF
            while (_buffer.Current.type != LexemeType.SEMICOLON &&
                   _buffer.Current.type != LexemeType.EOF)
            {
                _buffer.Next();
            }

            if (_buffer.Current.type == LexemeType.SEMICOLON)
            {
                _buffer.Next();
            }

            // Skip whitespace
            while (_buffer.Current.type == LexemeType.WHITESPACE)
            {
                _buffer.Next();
            }
        }
    }
}
