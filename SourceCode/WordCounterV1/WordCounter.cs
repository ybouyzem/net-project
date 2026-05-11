using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCounterV1
{
    public class WordCounter
    {
        private Dictionary<string, int> _wordFrequency;

        public WordCounter()
        {
            _wordFrequency = new Dictionary<string, int>();
        }
        public void AddWords(List<string> words)
        {
            foreach (string word in words)
            {
                if (_wordFrequency.ContainsKey(word))
                {
                    _wordFrequency[word]++;
                }
                else
                {
                    _wordFrequency[word] = 1;
                }
            }
        }
        public Dictionary<string, int> GetFrequencies()
        {
            return _wordFrequency
                .OrderByDescending(kv => kv.Value)
                .ThenBy(kv => kv.Key)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
        public void DisplayResults()
        {
            if (_wordFrequency.Count == 0)
            {
                Console.WriteLine("No words found.");
                return;
            }

            var sorted = GetFrequencies();

            Console.WriteLine("\n========== WORD FREQUENCY RESULTS ==========\n");

            Console.WriteLine("{0,-25} {1,10}", "Word", "Count");
            Console.WriteLine(new string('-', 38));

            foreach (KeyValuePair<string, int> entry in sorted)
            {
                Console.WriteLine("{0,-25} {1,10}", entry.Key, entry.Value);
            }

            Console.WriteLine(new string('-', 38));

            long totalWords = _wordFrequency.Values.Sum(x => (long)x);

            Console.WriteLine("{0,-25} {1,10}", "Unique words:", _wordFrequency.Count);
            Console.WriteLine("{0,-25} {1,10}", "Total words:", totalWords);

            Console.WriteLine("\n============================================");
        }
    }
}
