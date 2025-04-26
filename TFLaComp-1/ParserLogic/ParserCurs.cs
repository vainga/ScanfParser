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
            { LexemeType.SEMICOLON, new Regex(@"^;") }
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

        public Parser(List<Lexeme> lexemes)
        {
            _buffer = new LexemeBuffer(lexemes);
        }

        public void Parse()
        {
            // 1. Проверяем, что первый токен — это `scanf`
            if (_buffer.Current.type != LexemeType.SCANFCALL)
                throw new Exception("Ожидается вызов 'scanf'.");

            _buffer.Next();

            // 2. Проверяем открывающую скобку `(`
            if (_buffer.Current.type != LexemeType.L_BRACKET)
                throw new Exception("Ожидается '(' после 'scanf'.");

            _buffer.Next();

            // 3. Проверяем строку формата
            if (_buffer.Current.type != LexemeType.FORMAT_STRING)
                throw new Exception("Ожидается строка формата (например, \"%d\").");

            _buffer.Next();

            // 4. Обрабатываем аргументы
            while (_buffer.Current.type != LexemeType.R_BRACKET)
            {
                if (_buffer.Current.type == LexemeType.COMMA)
                {
                    _buffer.Next();
                }
                else if (_buffer.Current.type == LexemeType.AMPERSAND)
                {
                    _buffer.Next();

                    if (_buffer.Current.type != LexemeType.VARIABLE)
                        throw new Exception("Ожидается имя переменной после '&'.");

                    _buffer.Next();
                }
                else
                {
                    throw new Exception($"Неожиданный символ: {_buffer.Current.value}");
                }
            }

            // 5. Проверяем закрывающую скобку `)`
            if (_buffer.Current.type != LexemeType.R_BRACKET)
                throw new Exception("Ожидается ')' в конце вызова scanf.");

            _buffer.Next();

            // 6. Проверяем точку с запятой `;`
            if (_buffer.Current.type != LexemeType.SEMICOLON)
                throw new Exception("Ожидается ';' после вызова scanf.");

            Console.WriteLine("Синтаксический анализ завершен успешно! 🎉");
        }
    }
}
