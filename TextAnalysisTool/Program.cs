using System;
using System.Collections.Generic;

namespace TextAnalysisTool {
    class Program {

        public static void Init() {

            DefinitionService.Init();

            CorpusManager.Init();
        }


        private static void PrintHeader() {
            Console.WriteLine("{0}{1}{2}", "Word".PadRight(20), "Category".PadRight(12), "Frequency");
            Console.WriteLine("------------------------------------");
        }

        private static void PrintStats(Dictionary<string, WordStatistic> stats) {
            PrintHeader();
            foreach (var stat in stats.Values) {
                Console.WriteLine("{0}{1}{2}", stat.Word.PadRight(20), stat.Category.ToString().PadRight(12), stat.Count);
            }
        }


        private static void DefineWord(string word) {

            if (string.IsNullOrEmpty(word)) {
                Console.WriteLine("Input word is empty or null");
                return;
            }

            List<string> definition = CacheProvider.Instance.GetFromCache(word);
            if (definition == null) {
                Console.WriteLine("Input word is not found in Cache");
                return;
            }
            Console.WriteLine(string.Join("\n", definition));
        }

        static void Main(string[] args) {

            bool quitNow = false;

            Init();

            while (!quitNow) {

                Console.WriteLine();
                var cmdProcessor = new CmdLineProcessor(Console.ReadLine());

                switch (cmdProcessor.Command) {

                    case "help":
                        Console.WriteLine("analyze <sentence|essay>");
                        Console.WriteLine("define <word> after using analyze");
                        break;

                    case "analyze":
                        if (cmdProcessor.Sentence != null) {
                            Analyzer analyzer = new Analyzer();
                            analyzer.AnalyzeSentence(cmdProcessor.Sentence);
                            PrintStats(analyzer.GetStats());
                        }
                        if (cmdProcessor.EssayName != null) {
                            Analyzer analyzer = new Analyzer();
                            analyzer.AnalyzeEssay(cmdProcessor.EssayName);
                            PrintStats(analyzer.GetStats());
                        }
                        break;

                    case "define":
                        DefineWord(cmdProcessor.Word);
                        break;

                    case "exit":
                        quitNow = true;
                        break;

                    default:
                        Console.WriteLine("Type 'exit' to terminate");
                        break;
                }
            }

        }
    }
}
