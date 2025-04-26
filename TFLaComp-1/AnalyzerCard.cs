using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ScanfParser.DTO;

namespace ScanfParser
{
    internal class AnalyzerCard
    {
        private readonly List<CardDTO> _cards;


        private static readonly Dictionary<string, string> PaymentSystems = new()
        {
            { "4", "Visa" },
            { "5", "MasterCard" },
            { "6", "UnionPay" },
            { "2", "Мир" }
        };

        private static readonly Dictionary<string, string> BankCodes = new()
        {
            { "4276", "Сбербанк" },
            { "5469", "Сбербанк" },
            { "5213", "Тинькофф" },
            { "4377", "Тинькофф" },
            { "4584", "Альфа-Банк" },
            { "5486", "Альфа-Банк" },
            { "4242", "ВТБ" },
            { "4622", "ВТБ" },
            { "5547", "Газпромбанк" },
            { "5209", "Газпромбанк" },
            { "4627", "Райффайзенбанк" },
            { "5100", "Райффайзенбанк" },
            { "5179", "Открытие" },
            { "4478", "Промсвязьбанк" },
            { "5103", "Россельхозбанк" },
            { "5222", "Совкомбанк" },
            { "4038", "Хоум Кредит" },
            { "4890", "ЮниКредит Банк" },
            { "4154", "Росбанк" },
            { "5308", "Кредит Европа Банк" },
            { "6762", "Русский Стандарт" },
            { "4058", "МТС Банк" },
            { "4112", "Почта Банк" },
            { "4314", "МКБ" },
            { "4405", "Зенит" },
            { "4506", "Уралсиб" },
            { "4652", "Ак Барс" },
            { "4724", "Сетелем Банк" },
            { "4779", "Локо-Банк" },
            { "4893", "Дом.РФ" },
            { "5211", "Соверен Банк" },
            { "5254", "Энерготрансбанк" },
            { "5301", "Банк Санкт-Петербург" },
            { "5321", "Пересвет" },
            { "5425", "СМП Банк" },
            { "5481", "Новикомбанк" },
            { "5521", "Экспобанк" },
            { "5536", "Тинькофф" },
            { "5578", "Банк Финсервис" },
            { "5590", "Банк Оренбург" },
            { "5656", "Банк Казани" },
            { "6011", "Авангард" }
        };


        public AnalyzerCard(List<CardDTO> cards) 
        {
            _cards = cards;
        }

        private string GetPaymentSystem(CardDTO card)
        {
            string paymentSystemId = card.NumberCard.Substring(0, 1);
            if (PaymentSystems.TryGetValue(paymentSystemId, out string? value))
            {
                return value;
            }
            else
                return "Неизвестная платежная система";
        }

        private string GetBankCode(CardDTO card)
        {
            string bin = card.NumberCard.Substring(0, 4);
            
            if(BankCodes.TryGetValue(bin, out string? value))
            {
                return value;
            }
            else
                return "Неизвестный банк";
        }

        public List<FullCardDTO> Analyze()
        {

            List<FullCardDTO> fullCards = new List<FullCardDTO>();

            foreach (CardDTO card in _cards) 
            {
                FullCardDTO fullCardDTO = new FullCardDTO(card, GetPaymentSystem(card), GetBankCode(card));
                fullCards.Add(fullCardDTO);
            }

            return fullCards;
        }

    }
}
