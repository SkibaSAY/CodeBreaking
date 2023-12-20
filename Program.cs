using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeBreaking
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        static Regex letterRegex = new Regex(@"\W",RegexOptions.Compiled);
        static Regex spaceRegex = new Regex(@"\s+", RegexOptions.Compiled);
        public static string savePath = "save.json";
        static void Start()
        {
            SimpleReplaceCipher cipher = null;
            if (File.Exists(savePath))
            {
                var text = File.ReadAllText(savePath);
                cipher = JsonConvert.DeserializeObject<SimpleReplaceCipher>(text);
            }
            else
            {
                var text = "ылтесыдчлпкпеуаебхкузфлшмэлпуаебхубыэапеухъюэхлузцэаеюэгзгэфлжугсэпплугкъпефлглсдчхрмежвглтскцпэфкхкцхлшлпсэыэкхкшхъялфлжтлуглбпплжшсъугккццэтлхълтънеппдчсеупкйлпкукбхкфэфкагльлуьлскюеуфкаяхеуфлаеухкалщплгэфыдсэцкгзубглпеядхллгсэщепкещэсэмъоеыплшлкхккшсэрнешлыллясэщепкбглядхяхеуфтлмляпджяхеуфъшхэмфлжугэхклухеткгехзпджплчлхлмпджыцшхбмешлпетслмлхщкгехзпджплтслпкйэгехзпджкгбщехджлугэыхбхтлуеяепетскбгплеытеюэгхепкепеуфслаплшлылтслуэкалшядфэцэгзубмесцфкаеухкяпеядхуглхзсэыплмъоплутлфлепыуевгкцэаеюэпкбтскохкапепэъаалщегядгзглхзфлтлглаъюглбцпэхпефлглсдетлмсляплугкешлщкцпккалщегядгзпэмсъшлшлыкмешлтслкцыехядулыесоепплсэцхкюплеытеюэгхепкеплгэффэфыдлпеапеъухдокгепклгфлшлфслаеаепбглтлпеылхемлхщпдмлылхзугылыэгзубвгкакцлясэщепкеауфэщъыцэфхрюепкеюгллпядхыллянелюепзпемъсепккаехлмпъкцгечлскшкпэхзпдчькцклплакжфлглсделуляепплпсэыбгубщепнкпэауыегуфка";
                cipher = new SimpleReplaceCipher(text);

                var example = File.ReadAllText("Задачи\\Война_И_Мир.txt");
                example = letterRegex.Replace(example, " ");

                example = example.ToLower();
                var sb = new StringBuilder();
                foreach (var ch in example)
                {
                    var code = (int)ch;
                    if (code >= 1072 && code <= 1103 || code == 32)
                    {
                        sb.Append(ch);
                    }
                }

                example = sb.ToString();
                example = spaceRegex.Replace(example, " ");

                cipher.LoadTextExamle(example);
            }


            var controller = new ReplaceCipherController(cipher);
            controller.Start();

            SaveResult(cipher);
        }

        /// <summary>
        /// Сохранение шифра
        /// </summary>
        /// <param name="cipher"></param>
        public static void SaveResult(SimpleReplaceCipher cipher)
        {
            var json = JsonConvert.SerializeObject(cipher);
            File.WriteAllText(savePath, json);
        }
    }
}
