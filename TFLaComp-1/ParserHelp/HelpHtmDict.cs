using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanfParser.ParserHelp
{
    public static class HelpHtmDict
    {
        public static Dictionary<string, string> TopicDict = new()
        {
            ["Метод анализа"] = "analysis.htm",
            ["Диагностика"] = "diagnostics.htm",
            ["Правка"] = "edit.htm",
            ["Файл"] = "file.htm",
            ["Грамматика"] = "grammar.htm",
            ["Классификация"] = "grammarclass.htm",
            ["Справка"] = "help.htm",
            ["Список литературы"] = "literature.htm",
            ["Постановка задачи"] = "problem.htm",
            ["Исходный код"] = "sourcecode.htm",
            ["Пуск"] = "start.htm",
            ["Тестовый пример"] = "testexample.htm",
            ["Текст"] = "text.htm"
        };

        public static Dictionary<string, string> KeywordDict = new()
        {
            ["Метод анализа"] = "Метод анализа",
            ["Диагностика"] = "Диагностика и нейтрализация ошибок",
            ["Правка"] = "Правка",
            ["Файл"] = "Файл",
            ["Грамматика"] = "Грамматика",
            ["Классификация"] = "Классификация грамматики",
            ["Справка"] = "О программе",
            ["Список литературы"] = "Список литературы",
            ["Постановка задачи"] = "Постановка задачи",
            ["Исходный код"] = "Исходный код",
            ["Пуск"] = "Пуск",
            ["Тестовый пример"] = "Тестовый пример",
            ["Текст"] = "Текст",
            ["Вставить"] = "Вставить",
            // остальные одноименные
        };
    }
}
