using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBreaking
{
    /// <summary>
    /// Класс для консольного управления процессом декодирования
    /// </summary>
    public class ReplaceCipherController
    {
        private SimpleReplaceCipher Cipher;
        public ReplaceCipherController(SimpleReplaceCipher cipher)
        {
            Cipher = cipher;
        }

        public void Start()
        {
            GetInfo();

            WriteInformation("Начинаю процесс декодирования шифра простой замены\n\r");
            Cipher.InitStartDecodeTable();

            WriteEtalonStat();
            WriteDecodeTable();

            while (true)
            {
                WriteInformation($"Раскодированный текст: {Cipher.CurrentText}");

                WriteInformation("Введите команду: ");
                var command = Console.ReadLine();
                LogToFile(command);

                try
                {
                    var items = command.Split(new char[] { ' ' });

                    switch (items[0])
                    {
                        case "info":
                            GetInfo();
                            break; 
                        case "transp":
                            Transposition_a_b(items[1].First(), items[2].First());
                            break;
                        case "searchNext":
                            SearchNext(int.Parse(items[1]));
                            break;
                        case "back":
                            BackLast();
                            break;
                        case "show":
                            if(items[1] == "decodeTable")
                            {
                                WriteDecodeTable();
                            }
                            else if (items[1] == "defaultStats")
                            {
                                WriteDefaultStats();
                            }
                            break;
                        case "ready":
                            CharReady(items[1]);
                            break;
                        case "unready":
                            CharUnReady(items[1]);
                            break;
                        default:
                            WriteInformation("Ошибка ввода, повторите попытку");
                            break;
                    }
                }
                catch(Exception ex)
                {
                    WriteInformation("Произошла ошибка, попробуйте снова");
                }
                finally
                {
                    WriteInformation("\n\r");

                    Program.SaveResult(Cipher);
                }
            }
        }

        private void GetInfo()
        {
            var info = "------------Подсказка---------------\n\r" +
                "0.info - выводит подсказку\n\r" +
                "1.transp a b - транспозиция а б;\n\r" +
                "2.searchNext a - запускает итерацию поиска следующего варианта декодирования; a - шаг пар транспозиции\n\r" +
                "3.back -откат до предыдущего состояния;\n\r" +
                "4.show decodeTable -вывести таблицу декодирования;\n\r" +
                "5.show defaultStats\n\r" +
                "6.ready <char> -зафиксировать результат декодирования для конкретного слова\n\r" +
                "7.unready <char> -снять фиксацию\n\r";
            WriteInformation(info);
            WriteInformation("\n\r");
        }

        private string logFile = "log.txt";
        private void LogToFile(string text)
        {
            using(var sw = new StreamWriter(logFile, append: true))
            {
                sw.WriteLine(text);
            }
        }

        /// <summary>
        /// Запись протокола в консоль и в файл
        /// </summary>
        /// <param name="text"></param>
        private void WriteInformation(string text)
        {
            Console.WriteLine(text);
            LogToFile(text);
        }

        /// <summary>
        /// 1.Транспозиция а б
        /// </summary>
        private void Transposition_a_b(char a, char b)
        {
            Cipher.Transposition(char1: a, char2: b);
        }

        /// <summary>
        /// 2.Поиск следующего варианта расшифровки
        /// </summary>
        private void SearchNext(int itteration)
        {
            Cipher.SearchNextDecode(itteration);
        }

        /// <summary>
        /// 3.Откат назад
        /// </summary>
        private void BackLast()
        {
            WriteInformation("Произвожу откат до предыдущего состояния");
            Cipher.BackLastState();
            WriteInformation("\n\r");
        }

        /// <summary>
        /// 4.Вывести таблицу декодирования
        /// </summary>
        private void WriteDecodeTable()
        {
            WriteInformation("Вывожу таблицу декодирования");

            foreach (var kvp in Cipher.DecodeTable)
            {
                WriteInformation($"<{kvp.Key}> : {kvp.Value}");
            }
        }

        /// <summary>
        /// 5.Исходные распределения
        /// </summary>
        private void WriteDefaultStats()
        {
            WriteInformation("Распределение в закодированном тексте");
            foreach (var kvp in Cipher.LettersInCodedText)
            {
                WriteInformation($"<{kvp.Key}> : {kvp.Value}");
            }

            WriteInformation("\n\r");

            WriteEtalonStat();
        }

        /// <summary>
        /// 6.Фиксация значения
        /// </summary>
        private void CharReady(string char1)
        {
            foreach(var ch in char1)
            {
                Cipher.ReadyChar(ch.ToString());
            }
        }

        /// <summary>
        /// 7.Открепление значения
        /// </summary>
        private void CharUnReady(string char1)
        {
            Cipher.UnreadyChar(char1);
        }

        private void WriteEtalonStat()
        {
            WriteInformation("Вывожу статистическое распределение символов исходного языка\n\rChar:    Count:");
            foreach (var kvp in Cipher.Letters.OrderByDescending(kpv => kpv.Value))
            {
                WriteInformation($"{kvp.Key} : {kvp.Value}");
            }
            WriteInformation("\n\r");
        }
    }
}
