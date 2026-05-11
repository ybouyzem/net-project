using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCounterV2
{
public class WordCounter
{
    private Dictionary<string, int> _wordStats;

    public WordCounter()
    {
        _wordStats = new Dictionary<string, int>();
    }

    public void AddWords(List<string> wordList)
    {
        foreach (string currentWord in wordList)
        {
            if (_wordStats.TryGetValue(currentWord, out int count))
            {
                _wordStats[currentWord] = count + 1;
            }
            else
            {
                _wordStats.Add(currentWord, 1);
            }
        }
    }

    public Dictionary<string, int> GetFrequencies()
    {
        return _wordStats
            .OrderByDescending(item => item.Value)
            .ThenBy(item => item.Key)
            .ToDictionary(item => item.Key, item => item.Value);
    }

    public void DisplayResults()
    {
        if (_wordStats.Count == 0)
        {
            Console.WriteLine("No data available.");
            return;
        }

        Dictionary<string, int> orderedWords = GetFrequencies();

        Console.WriteLine("\n=======================================");
        Console.WriteLine("         WORD ANALYSIS REPORT");
        Console.WriteLine("=======================================");
        Console.WriteLine("{0,-25} {1,8}", "Word", "Count");
        Console.WriteLine("---------------------------------------");

        foreach (var pair in orderedWords)
        {
            Console.WriteLine("{0,-25} {1,8}", pair.Key, pair.Value);
        }

        Console.WriteLine("---------------------------------------");

        int uniqueWords = _wordStats.Count;
        long totalWordOccurrences = _wordStats.Values.Sum(value => (long)value);

        Console.WriteLine("{0,-25} {1,8}", "Unique Words:", uniqueWords);
        Console.WriteLine("{0,-25} {1,8}", "Total Words:", totalWordOccurrences);

        Console.WriteLine("=======================================");
    }
}
}
