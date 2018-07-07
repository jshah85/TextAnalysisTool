namespace TextAnalysisTool {
    public class WordStatistic {

        public string Word { get; private set; }

        public int Count { get; private set; }

        public CorpusManager.WordCategory Category { get; set; }

        public WordStatistic(string word) {
            Word = word;
            Count = 1;
            // set not found as default
            Category = CorpusManager.WordCategory.NotFound;
        }

        public void IncrementCount() {
            Count++;
        }
        
    }
}
