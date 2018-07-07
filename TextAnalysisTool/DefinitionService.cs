using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TextAnalysisTool {
    public class DefinitionService {

        private static readonly Dictionary<string, string> HtmlFilesDictionary = new Dictionary<string, string>();

        private static readonly Dictionary<string, List<string>> WordDefinitionDict = new Dictionary<string, List<string>>();

        public Dictionary<string, List<string>> Dictionary { get { return WordDefinitionDict; } }

        /// <summary>
        /// Initialize routine to process each html dictionary file and populate word definition dictionary
        /// </summary>
        public static void Init() {

            string directoryPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "OxfordDictionary");

            // Read entire content of html file as a string 
            foreach (var fileName in GetFileNames(directoryPath)) {

                string alphabet = Regex.Replace(fileName, "^.*_([a-z]{1})\\.html$", "$1");

                using (var fileStream = new FileStream(fileName, FileMode.Open)) {
                    
                    using (var reader = new StreamReader(fileStream)) {
                        HtmlFilesDictionary.Add(alphabet, reader.ReadToEnd());
                    }
                }
            }


            // Parse each html file's data and popualte defintion dictionary for every word. 115328 unique word size
            foreach (var htmlContent in HtmlFilesDictionary.Values) {

                var matchCollection = Regex.Matches(htmlContent, "<P><B>(.*?)<\\/B>\\s*\\(<I>(.*?)<\\/I>\\)\\s*(.*?)<\\/P>");

                foreach (Match match in matchCollection) {

                    var word = match.Groups[1].ToString().ToLower();

                    var sb = new StringBuilder();

                    sb.Append("(").Append(match.Groups[2]).Append(") ").Append(match.Groups[3]);

                    if (!WordDefinitionDict.ContainsKey(word)) {
                        WordDefinitionDict.Add(word, new List<string> {sb.ToString()});
                    }
                    else {
                        WordDefinitionDict[word].Add(sb.ToString());
                    }
                }
            }
            
        }

        private static string[] GetFileNames(string path) {

            string[] dirs = Directory.GetFiles(path, "*");

            return dirs;
        }
    }
}
