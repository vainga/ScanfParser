using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanfParser.DTO
{
    public class FullCardDTO
    {
        private readonly CardDTO _card;

        public string PaymentSystem { get; private set; }
        public string Bank { get; private set; }

        public string NumberCard => _card.NumberCard;
        public int IndexStart => _card.IndexStart;
        public int IndexEnd => _card.IndexEnd;

        public FullCardDTO(CardDTO card, string paymentSystem, string bank)
        {
            _card = card;
            PaymentSystem = paymentSystem;
            Bank = bank;
        }
    }
}
