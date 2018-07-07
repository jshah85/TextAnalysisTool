using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;

namespace TextAnalysisTool {
    public class Analyzer
    {

        private Dictionary<string, WordStatistic> _stats;
        private DefinitionService _definitionService;

        public Analyzer() {
            _stats = new Dictionary<string, WordStatistic>();
            _definitionService = new DefinitionService();
        }
        
        /// <summary>
        /// Return analysis statistics 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, WordStatistic> GetStats() {
            return _stats;
        }

        /// <summary>
        /// Analyze input string (line) and generate its state and populate in-memory Cache
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public Dictionary<string, WordStatistic> AnalyzeSentence(string line) {

            // Return if input string is empty or null
            if (string.IsNullOrEmpty(line)) return _stats;

            List<CacheItem> cacheItemList = new List<CacheItem>();

            foreach (var word in RegexStore.SanitizeSentence(line)) {
                if (_stats.ContainsKey(word)) {
                    _stats[word].IncrementCount();
                }
                else {
                    var wStat = CorpusManager.GetWordStatistic(word);
                    _stats.Add(word, wStat);
                }

                // Generate CacheItem for each word
                if (_definitionService.Dictionary.ContainsKey(word)) {
                    cacheItemList.Add(new CacheItem(word, _definitionService.Dictionary[word]));
                }
            }

            if (cacheItemList.Count > 0) {
                CacheProvider.Instance.AddToCache(cacheItemList);
            }

            return _stats;
        }

        
        /// <summary>
        /// Process multi line essay
        /// </summary>
        /// <param name="essayName"></param>
        public void AnalyzeEssay(string essayName) {

            try {
                var essayLines = EssayManager.ReadEssayFile(essayName);
                essayLines.ForEach(line => AnalyzeSentence(line));
            }
            catch (FileNotFoundException ex) {
                Console.WriteLine("Essay file not found");
            }
            catch (Exception ex) {
                Console.WriteLine("Exception during analyzing essay");
            }

        }
    }
}
