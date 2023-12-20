using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViginerCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            //t = 8 key = вупцуайу
            var encryptedText = "ёялуюнёы,рпбчроп.аюэуржво.ыы,сфу.фз.рэщ.ч.щурчэхугсвросярфыы,иёрсбюёьвжхтутцрзйвфус.плшржцюуыаклду.прсл.зртвбеиржцюу,аслдуыярвжаряъжрнйи.юэтыьзрноряяижше.мяргшбёьыявьжьосмярочрглыучокб.ьм,боь.мр,врсл.к шуаохюрхзаь.жше.мвге с...гбальдрув а.,кёмшрмшвмхафрроикямввтйгювлух хлулвуцоъучршуыааьыу.приярё.мг.схшё,ф ркъул,юегижюппцбу цутмлу,ежврцыциаогупмжшхйгюрсашсьш.вмгяецпп,ш,умтркрюегапгупмероьжр оуаоыяжрюёкесчврэяюохдъюшувожвфу о  ф,,ыфарсхдщуфёвяждёу фрижше.мгшрохрылёрвжфрцакуршхртмёбиж,жчф.ь ъуйфшёлйжавбп.ьчш .юэтыьжяжъшёрвжфрцакуршхжсмбукш,жжмв, э кбпыг,жажбфърсцштгисрпшат.,ях щбрйфбьяжд.чюкшрттм,лэ,аж вбитрпшвншмг.хшбр,мвгцйру.рябаогупмшлеяуфммяы к.еддцболу.хма.сфххсмб. к.еддцболвмьфуюровфмлбш чш.ёюёптжхэааеюаьм.юэтънэт.выжзау,ррсуфомдщу вхеж.мучохаогупмд.сь.дсм.шгф..д,аьртхъьщу урьм.сфрижюппцбу ц.иш.удеяуфмоуь ш,всмярнтюряп рдэ во.у. л.ншмгбольёшэяп,ждуг вьв.шлрширвыгтшджт";
            //t = 5 key = слово
            //var encryptedText = @"влцдутжбюцхъяррмшбрхцэооэцгбрьцмйфктъъюьмшэсяцпунуящэйтаьэдкцибрьцгбрпачкъуцпъбьсэгкцъгуущарцёэвърюуоюэкааэбрняфукабъарпяъафкъиьжяффнйояфывбнэнфуюгбрьсшьжэтбэёчюъюръегофкбьчябашвёэуъъюаднчжчужцёэвлрнчулбюпцуруньъшсэюъзкцхъяррнрювяспэмасчкпэужьжыатуфуярюравртубурьпэщлафоуфбюацмнубсюкйтаьэдйюнооэгюожбгкбрънцэпотчмёодзцвбцшщвщепчдчдръюьскасэгъппэгюкдойрсрэвоопчщшоказръббнэугнялёкьсрбёуыэбдэулбюасшоуэтъшкрсдугэфлбубуъчнчтртпэгюкиугюэмэгюккъъпэгяапуфуэзьрадзьжчюрмфцхраююанчёчюъыхьъцомэфъцпоирькнщпэтэузуябащущбаыэйчдфрпэцъьрьцъцпоилуфэдцойэдятррачкубуфнйтаьэдкцкрннцюабугюуубурьпйюэъжтгюркующоъуфъэгясуоичщщчдцсфырэдщэъуяфшёчцюйрщвяхвмкршрпгюопэуцчйтаьэдкцибрьцыяжтюрбуэтэбдуящэубъибрювъежагибрбагбрымпуноцшяжцечкфодщоъчжшйуъцхчщвуэбдлдъэгясуахзцэбдэулькнъщбжяцэьрёдъьвювлрнуяфуоухфекьгцчччгэъжтанопчынажпачкъуъмэнкйрэфщэъьбудэндадъярьеюэлэтчоубъцэфэвлнёэгфдсэвэёкбсчоукгаутэыпуббцчкпэгючсаъбэнэфъркацхёваетуфяепьрювържадфёжбьфутощоявьъгупчршуитеачйчирамчюфчоуяюонкяжыкгсцбрясшчйотъъжрсщчл";

            Start(encryptedText);
        }
        /// <summary>
        /// Исходный алфавит
        /// </summary>
        static List<char> A = new List<char>() {'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', ' ', ',', '.'};
        //static List<char> A = new List<char>() {'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'/*, ' ', ',', '.'*/};
        
        static void Start(string inputText)
        {
            var len = inputText.Length;

            //Выяснение длины ключа
            var t = 1;
            char[][] arr = null;
            for (; ; t++)
            {
                arr = PrepareArr(inputText, t);
                var indexOfSovpadenia = ComputeIndexOfSovpadenia(arr[0]);
                Console.WriteLine($"Длина ключа t = {t}; Индекс совпадения = {indexOfSovpadenia}; Идём дальше? нет/другое");

                var answer = Console.ReadLine();
                if (answer.Equals("нет"))
                {
                    break;
                }
            }

            Console.WriteLine($"Приступаем к шагу 2: длина ключа t = {t} Решаем шифр цезаря в каждой строке");

            //шаг 2: Найти для каждой строки кодирующий символ
            var sdvigOnStaticRow = new int[arr.Count()];
            var staticRow = arr[0];
            for(var i = 1; i < arr.Count(); i++)
            {
                var row = arr[i];
                var sdvig = FindMostExpectedSdvig(staticRow, row);
                sdvigOnStaticRow[i] = sdvig;
            }

            //перебираем все символы А в качестве начальных, формируем ключи, декодируем и смотрим результат
            var keys = new List<string>();
            foreach(var a in A)
            {
                var sb = new StringBuilder();
                sb.Append(a);

                for (var i = 1; i < arr.Count(); i++)
                {
                    var sdvig = sdvigOnStaticRow[i];
                    sb.Append(A[(A.Count() + A.IndexOf(a) - sdvig) % A.Count()]);
                }
                keys.Add(sb.ToString());
            }

            //Декодирование полученными ключами
            Console.WriteLine("Начинаем стадию декодирования: Вам потребуется выбрать подходящий вариант раскодирования из предложенных.");

            var keyNumber = 1;
            foreach(var aplicantKey in keys)
            {
                var decodedText = Decrypt(inputText, aplicantKey);

                Console.WriteLine($"{keyNumber}.Ключ {aplicantKey} \n\rТекст: {decodedText}\n\r");
                keyNumber++;
            }

            //итоговый вывод
            //Console.Write("Введите номер подходящего текста: ");
            //var userAnswer = Console.ReadLine();


        }
        static char[][] PrepareArr(string inputStr, int t)
        {
            var messageLen = inputStr.Length;
            var rowLen = messageLen / t;
            var arr = new char[t][];

            //заполняем построчно
            for(var i = 0; i < t; i++)
            {
                arr[i] = new char[rowLen];
                for(var j = 0; j < rowLen; j++)
                {
                    var currIndex = i + j * t;
                    if(currIndex < messageLen)
                    {
                        arr[i][j] = inputStr[currIndex];
                    }
                    //никто не сказал, что размер ключа делит число символов в исходном тексте
                    else
                    {
                        //arr[i][j] = '-';
                    }
                }
            }

            return arr;
        } 

        /// <summary>
        /// Считаем индекс совпадения строки
        /// </summary>
        /// <param name="currentRow"></param>
        /// <returns></returns>
        static double ComputeIndexOfSovpadenia(char[] currentRow)
        {
            double result = 0;

            //пробегаем по всему алфавиту и считаем индекс совпадения
            foreach(var a in A)
            {
                var countOfExist = currentRow.Count(x => x.Equals(a));
                if(countOfExist > 1)
                {
                    result += countOfExist * 1.0 / currentRow.Length * (countOfExist - 1) / (currentRow.Length - 1);
                }
            }

            return result;
        }
        
        /// <summary>
        /// Статистика по взаимным совпадениям для сдвигов в русском алфавите
        /// </summary>
        static Dictionary<int, double> SdvigStatistics = new Dictionary<int, double>
        {
            { 0,0.0553 },
            { 1,0.0366 },
            { 2,0.0345 },
            { 3,0.0400 },
            { 4,0.0340 },
            { 5,0.0360 },
            { 6,0.0326 },
            { 7,0.0241},
            { 8,0.0287 },
            { 9,0.0317},
            { 10,0.0265 },
            { 11,0.0251 },
            { 12,0.0244 },
            { 13,0.0291 },
            { 14,0.0322 },
            { 15,0.0244 },
            { 16,0.0249}
        };

        /// <summary>
        /// Поиск сдвига строки Перебор сдвигов фиксированной строки
        /// </summary>
        /// <param name="currentRow"></param>
        /// <returns></returns>
        static int FindMostExpectedSdvig(char[] staticRow, char[] currentRow)
        {
            var minSdvig = 0;//SdvigStatistics.Keys.Min();
            var maxSdvig = A.Count - 1;//SdvigStatistics.Keys.Max();

            var maxVzIndex = 0.0;
            var mostExpectedSdvig = -1;
            for (var i = minSdvig; i <= maxSdvig; i++)
            {
                var currentVzIndex = ComputeVzaimniyIndex(staticRow, currentRow, i);

                if(maxVzIndex < currentVzIndex)
                {
                    maxVzIndex = currentVzIndex;
                    mostExpectedSdvig = i;
                }
            }

            return mostExpectedSdvig;
        }

        /// <summary>
        /// Взаимный индекс строки и её сдвига
        /// </summary>
        /// <param name="currentRow"></param>
        /// <param name="sdvig"></param>
        /// <returns></returns>
        static double ComputeVzaimniyIndex(char[] staticRow, char[] currentRow, int sdvig)
        {
            double result = 0;

            var sdvigRow = currentRow.Select(x=>A[(A.Count + A.IndexOf(x) + sdvig) % A.Count]).ToArray();
            
            foreach(var a in A)
            {
                var countOfExistStaticRow = staticRow.Count(x => x.Equals(a));

                var countOfExistSdvigRows = sdvigRow.Count(x => x.Equals(a));

                result += countOfExistStaticRow * countOfExistSdvigRows / 1.0 / currentRow.Length / sdvigRow.Length;
            }

            return result;
        }

        static string Decrypt(string cipherText, string key)
        {
            var sb = new StringBuilder();
            for(var i = 0; i < cipherText.Length; i++)
            {
                var codedChar = cipherText[i];
                var keyIndex = i % key.Length;
                var deCoddedChar = A[(A.Count + A.IndexOf(codedChar) - A.IndexOf(key[keyIndex])) % A.Count];
                sb.Append(deCoddedChar);
            }

            var result = sb.ToString();
            return result;
        }
    }
}
