using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextAnalysisTool {
    public class CorpusManager {

        private const char NewLineChar = '\n';

        public enum WordCategory
        {
            Large,
            Medium,
            Short,
            NotFound
        }

        public static HashSet<string> LargeWordSet { get; private set; }
        public static HashSet<string> MediumWordSet { get; private set; }
        public static HashSet<string> ShortWordSet { get; private set; }

        public static void Init() {
            ReadLargeFile();
            ReadMediumFile();
            ReadShortFile();
        }

        private static void ReadLargeFile() {
            var largeWordsList = ReadFile("long.txt");
            LargeWordSet = new HashSet<string>(largeWordsList);
        }

        private static void ReadMediumFile() {
            var mediumWordsList = ReadFile("medium.txt");
            MediumWordSet = new HashSet<string>(mediumWordsList);
        }

        private static void ReadShortFile() {
            var shortWordsList = ReadFile("short.txt");
            ShortWordSet = new HashSet<string>(shortWordsList);
        }

        public static List<string> ReadFile(string fileName) {
            List<string> rows = null;

            var rootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var templateFilePath = string.Format(@"Corpus\{0}", fileName);
            var templatePath = Path.Combine(rootPath, templateFilePath);

            try {
                // Open the text file using a stream reader.
                using (StreamReader reader = new StreamReader(templatePath)) {
                    rows = reader.ReadToEnd().Split(NewLineChar).ToList();
                }
            }
            catch (Exception e) {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return rows;
        }


        public static WordStatistic GetWordStatistic(string word) {

            WordStatistic stat = new WordStatistic(word);
            if (LargeWordSet.Contains(word)) {
                stat.Category = WordCategory.Large;
            }
            else if (MediumWordSet.Contains(word)) {
                stat.Category = WordCategory.Medium;
            }
            else if (ShortWordSet.Contains(word)) {
                stat.Category = WordCategory.Short;
            }
            return stat;
        }
    }
}
