using CodeBreaking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeBreaking
{
    public class SimpleReplaceCipher
    {
        public string InputText { get; set; }
        public string CurrentText
        {
            get
            {
                return String.Join("", InputText.Select(ch => DecodeTable[ch].OutputChar.Value));
            }
        }
        /// <summary>
        /// Для быстрого поиска по таблице раскодирования
        /// Key = Value.InputChar
        /// </summary>
        public Dictionary<char, DecodedChar> DecodeTable { get; set; }

        /// <summary>
        /// Статистика распределения по Кодированному тексту
        /// </summary>
        public ConcurrentDictionary<string, int> LettersInCodedText { get; set; }
        public ConcurrentDictionary<string, int> Letters { get; set; }

        /// <summary>
        /// Различные <=i граммы, i зависит от начальных настроек
        /// </summary>
        public ConcurrentDictionary<string, int> ExpectedGrams { get; set; }

        public SimpleReplaceCipher()
        {

        }
        public SimpleReplaceCipher(string codedText)
        {
            InputText = codedText;
            Letters = new ConcurrentDictionary<string, int>();
            ExpectedGrams = new ConcurrentDictionary<string, int>();

            LettersInCodedText = GetGram(codedText);
        }



        #region LoadStatistic
        /// <summary>
        /// Загружает реальный текст и добавляет статистику, собранную из него
        /// </summary>
        /// <param name="realText"></param>
        /// <param name="refreshStatistic">сброс прошлой статистики</param>
        public void LoadTextExamle(string realText, bool refreshStatistic = false)
        {
            //слова нужны для определения корректности полученного текста
            var words = GetAllWords(realText);

            var gram = GetGram(realText, 1);
            var bigram = GetGram(realText, 2);
            var treeGram = GetGram(realText, 3);

            AddOrUpdateDict(Letters, gram, refreshStatistic);
            AddOrUpdateDict(ExpectedGrams, bigram, refreshStatistic);
            AddOrUpdateDict(ExpectedGrams, treeGram, refreshStatistic);
        }

        private void AddOrUpdateDict(ConcurrentDictionary<string, int> target, ConcurrentDictionary<string, int> sourceDict, bool refresh)
        {
            foreach (var item in sourceDict)
            {
                target.AddOrUpdate(item.Key, item.Value, (key, oldValue) =>
                {
                    return oldValue + item.Value;
                });
            }
        }

        private ConcurrentDictionary<string, int> GetGram(string text, int gramLen = 1)
        {
            var dict = new ConcurrentDictionary<string, int>();

            Parallel.For(0, text.Length - gramLen,new ParallelOptions { MaxDegreeOfParallelism = 10 }, i =>
            {
                var substring = text.Substring(i, gramLen);
                if(substring.Contains(" "))
                {
                    return;
                }
                dict.AddOrUpdate(substring, 1,(key,oldValue) => {
                    return oldValue + 1;
                });
            });

            if(gramLen > 1)
            {
                var graniza = (dict.Sum(kvp => kvp.Value)) / 1000;
                dict.Where(kvp => kvp.Value < graniza).ToList().ForEach(kvp => dict.TryRemove(kvp));
            }

            return dict;
        }

        private static Regex wordRegex = new Regex(@"\w+");
        private ConcurrentDictionary<string, int> GetAllWords(string text)
        {
            var dict = new ConcurrentDictionary<string, int>();

            foreach(Match match in wordRegex.Matches(text))
            {
                var word = match.Value;

                dict.AddOrUpdate(word, 1, (key, oldValue) =>
                {
                    return oldValue + 1;
                });
            }

            return dict;
        }
        #endregion

        private DecodedChar FindByDecode(char char1)
        {
            return DecodeTable.Where(kvp => kvp.Value.OutputChar.Value.Equals(char1)).First().Value;
        }
        #region UseStatistics 
        public void Transposition(char char1, char char2)
        {
            MoveLast.Clear();

            var decoded1 = FindByDecode(char1);
            var decoded2 = FindByDecode(char2);

            //сохраняем изменение для возможного отката
            MoveLast.Add(char1, decoded1.OutputChar);
            MoveLast.Add(char2, decoded2.OutputChar);

            var temp = decoded1.OutputChar;
            decoded1.OutputChar = decoded2.OutputChar;
            decoded2.OutputChar = temp;
        }

        /// <summary>
        /// Оценка нынешнего правдоподобия, чем больше, чем ближе к реальному тексту
        /// </summary>
        public int Total { get; private set; }

        /// <summary>
        /// Возращает насколько изменился Total из-за транспозиции
        /// </summary>
        /// <param name="ch1"></param>
        /// <param name="ch2"></param>
        /// <returns></returns>
        private int Transposition(DecodedChar ch1, DecodedChar ch2)
        {
            var lastTotal = Total;

            var temp = ch1.OutputChar;
            ch1.OutputChar = ch2.OutputChar;
            ch2.OutputChar = temp;

            var newTotal = TrueFunction(CurrentText);
            Total = newTotal;

            var result = newTotal - lastTotal;
            return result;
        }

        public void ReadyChar(string char1)
        {
            var finded = DecodeTable.Where(kvp=>kvp.Value.OutputChar.Value.Equals(char1.First())).First().Value;
            finded.Ready();
        }

        public void UnreadyChar(string char1)
        {
            var finded = DecodeTable.Where(kvp => kvp.Value.Equals(char1)).First().Value;
            finded.UnReady();
        }

        public void SearchNextDecode(int iterationCount)
        {
            var undecodedItems = DecodeTable.Values.Where(v => !v.IsReady).ToList();          

            for(var i = 0; i < undecodedItems.Count - iterationCount; i++)
            {
                var current = undecodedItems[i];
                var applicant = undecodedItems[i + iterationCount];
                var success = TryTranstosition(current, applicant);
            }
        }

        /// <summary>
        /// Выбирает новую подстановку
        /// stabilizeKf - для того, чтобы выровнять число элементов в сравниваемых выборках = CountTeoreticalStatistic/CountCurretnStatistic
        /// </summary>
        public bool TryTranstosition(DecodedChar current, DecodedChar applicant)
        {
            var currOut = current.OutputChar.Value;
            //защита от дурака
            if (current.IsReady)
            {
                return false;
            }

            var totalExchange = Transposition(current, applicant);

            if (totalExchange <= 0)
            {
                //откат
                Transposition(applicant, current);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Возвращает значение, показывающее правдоподобность текста
        /// </summary>
        /// <param name="testedTest"></param>
        /// <returns></returns>
        public int TrueFunction(string testedTest)
        {
            var totalExp = 0.0;

            foreach(var kvp in ExpectedGrams)
            {
                var gram = kvp.Key;
                var count = kvp.Value;
                
                for(var i = 0; i < testedTest.Length - gram.Length; i++)
                {
                    var current = testedTest[i];
                    var eqCount = 0;
                    for(var j = 0; j < gram.Length; j++)
                    {
                        if(testedTest[i+j] == gram[j])
                        {
                            eqCount++;
                        }
                    }
                    var currentExp = eqCount == 0 ? 0 : Math.Pow(10, eqCount-1);
                    totalExp += currentExp;
                }
            }
            return (int)totalExp;
        }


        //TODO:хотел спрятать в private, но сериализация мешает
        public Dictionary<char, CharItem> MoveLast = new Dictionary<char, CharItem>();

        /// <summary>
        /// Откат до предыдущего состояния
        /// </summary>
        public void BackLastState()
        {
            foreach(var kvp in MoveLast)
            {
                DecodeTable[kvp.Key].OutputChar = kvp.Value;
            }
        }

        /// <summary>
        /// Задаёт базовое распределение 
        /// </summary>
        public void InitStartDecodeTable()
        {
            var codedInputLetters = LettersInCodedText.OrderByDescending(kvp => kvp.Value).ToList();
            var statisticLetters = Letters.OrderByDescending(kvp => kvp.Value).ToList();

            DecodeTable = new Dictionary<char, DecodedChar>();
            for(var i = 0; i < statisticLetters.Count; i++)
            {
                //значит, в входном тексте меньше символов, чем в статистике
                if (i < codedInputLetters.Count)
                {
                    var decodeItem = new DecodedChar
                    {
                        InputChar = new CharItem { Value = codedInputLetters[i].Key[0], Count = codedInputLetters[i].Value },
                        OutputChar = new CharItem { Value = statisticLetters[i].Key[0], Count = statisticLetters[i].Value },
                    };
                    DecodeTable.Add(codedInputLetters[i].Key.First(), decodeItem);
                }
            }
            Total = TrueFunction(CurrentText);
        }
        #endregion

    }
}
