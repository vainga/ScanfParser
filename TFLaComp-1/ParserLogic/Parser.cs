using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ScanfParser.DTO;
using static System.Net.Mime.MediaTypeNames;

namespace ScanfParser.RegExParser
{
    public class Parser
    {
        public List<CardDTO> Parse(string input) {
            List<CardDTO> cards = new List<CardDTO>();

            input = input.Trim();

            string patternWithout = "\\d{16}";
            string patternWithSpaces = "\\d{4}( \\d{4}){3}";

            string pattern = @"(?<=\s|^)(\d{16}|(\d{4}( \d{4}){3}))(?=\s|$)";

            Match match = Regex.Match(input, pattern);
            while (match.Success)
            {
                string value = match.Value;
                value = value.Trim();
                if (value.Contains(' ') == false)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        value = value.Insert(4 + i * 4 + i, " ");
                    }
                }

                CardDTO card = new CardDTO(value, match.Index, match.Index + match.Value.Length - 1);
                cards.Add(card);
                match = match.NextMatch();
            }

            return cards;
        }
    }
}
