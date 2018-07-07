using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextAnalysisTool {
    class EssayManager {

        private const char NewLineChar = '\n';

        public static List<string> ReadEssayFile(string fileName) {

            List<string> rows = new List<string>();

            var rootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var templateFilePath = string.Format(@"Corpus\EssayFiles\{0}", fileName);
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
            return rows.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

    }
}
