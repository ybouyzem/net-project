using System;
using System.Collections.Generic;

namespace WordCounterV2
{
    public class TextProcessor
    {
        private int _n; // Minimum length to trigger trimming
        private int _m; // Number of characters to remove from the end

        private static readonly char[] Separators = new char[]
        {
            ' ', '\t', '\n', '\r',
            '.', ',', ';', ':', '!', '?',
            '"', '\'', '(', ')', '[', ']', '{', '}',
            '-', '_', '/', '\\', '|', '@', '#', '%',
            '*', '+', '=', '<', '>', '~', '`', '^', '&'
        };

        /// <param name="n"></param>
        /// <param name="m"></param>
        public TextProcessor(int n, int m)
        {
            _n = n;
            _m = m;
        }
        public List<string> GetWords(string text)
        {
            var words = new List<string>();

            if (string.IsNullOrWhiteSpace(text))
                return words;

            string[] tokens = text.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string token in tokens)
            {
                string word = token.ToLower().Trim();

                if (string.IsNullOrEmpty(word))
                    continue;

                word = ApplyTrimRule(word);

                if (!string.IsNullOrEmpty(word))
                    words.Add(word);
            }

            return words;
        }

        private string ApplyTrimRule(string word)
        {
            if (word.Length > _n)
            {
                int newLength = word.Length - _m;
                if (newLength <= 0)
                    return string.Empty; 
                return word.Substring(0, newLength);
            }
            return word;
        }

        public string GetRuleDescription()
        {
            return $"Words longer than {_n} chars → last {_m} char(s) removed";
        }
    }
}
