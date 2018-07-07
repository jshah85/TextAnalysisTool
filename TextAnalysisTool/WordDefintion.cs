using System.Collections.Generic;

namespace TextAnalysisTool {
    public class WordDefintion{

        public string Word { get; }
        public List<string> Definition { get; }

        public WordDefintion(string word, List<string> definition) {
            Word = word;
            Definition = definition;
        }
        

    }
}
