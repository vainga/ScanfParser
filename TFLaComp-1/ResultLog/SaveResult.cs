using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScanfParser.DTO;

namespace ScanfParser.ResultLog
{
    public class SaveResult : ISaveResult
    {
        private readonly string _logFilePath = "resultlog.txt";
        private readonly string _countFilePath = "count.txt";
        private int _runCount;

        public SaveResult()
        {
            _runCount = LoadRunCount();
        }

        private int LoadRunCount()
        {
            if (File.Exists(_countFilePath) && int.TryParse(File.ReadAllText(_countFilePath), out int count))
            {
                return count;
            }
            return 0;
        }

        private void SaveRunCount()
        {
            File.WriteAllText(_countFilePath, _runCount.ToString());
        }

        public void WriteToLog(List<FullCardDTO> cards)
        {
            try
            {
                _runCount++;
                SaveRunCount();

                StringBuilder logEntry = new StringBuilder();
                logEntry.AppendLine($"Запуск {_runCount}:");

                foreach (var card in cards)
                {
                    logEntry.AppendLine($"Карта: {card.NumberCard}, Индексы: {card.IndexStart}-{card.IndexEnd}");
                    logEntry.AppendLine($"Банк: {card.Bank}, Платежная система: {card.PaymentSystem}");
                }

                logEntry.AppendLine(new string('-', 40));

                File.AppendAllText(_logFilePath, logEntry.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в лог-файл: {ex.Message}");
            }
        }
    }
}
