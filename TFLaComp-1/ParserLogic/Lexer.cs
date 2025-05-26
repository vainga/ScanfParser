using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScanfParser.ParserLogic
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
        UNKNOWN,
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


    public class Lexer
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
                    if (pattern.Key == LexemeType.WHITESPACE)
                        continue;

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
                    lexemes.Add(new Lexeme(LexemeType.UNKNOWN, expText[pos].ToString()));
                    pos++;
                }
            }

            lexemes.Add(new Lexeme(LexemeType.EOF, ""));
            return lexemes;
        }

    }

    public class LexemeBuffer
    {
        public readonly List<Lexeme> _lexemes;
        public int _position;

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
}