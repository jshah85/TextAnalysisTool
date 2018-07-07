using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextAnalysisTool {
    class RegexStore {

        // match punctuation characters colon, comma, tab, carriage return
        public static readonly string PunctuationPattern = "[:,-.\"*@'\t\r\n ]";


        /// <summary>
        /// Sanitize input sentence by removing unwanted non-alphabetic characters
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public static List<string> SanitizeSentence(string sentence) {

            List<string> output = new List<string>();

            if (string.IsNullOrEmpty(sentence)) {
                return output;
            }

            output.AddRange(sentence.Split(' ').Select(word => Regex.Replace(word, @"[^a-zA-Z]+", "").ToLower()));

            return output;
        }
    }
}
