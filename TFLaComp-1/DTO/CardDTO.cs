using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanfParser.DTO
{
    public class CardDTO
    {
        public string NumberCard { get; private set; }
        public int IndexStart { get; private set; }
        public int IndexEnd { get; private set; }
        public CardDTO(string numberCard, int start, int end)
        {
            NumberCard = numberCard;
            IndexStart = start;
            IndexEnd = end;
        }
    }
}
