using System;
using System.Collections.Generic;

namespace WordCounterV1
{
    public class TextProcessor
    {
        private static readonly char[] Separators = new char[]
        {
            ' ', '\t', '\n', '\r',
            '.', ',', ';', ':', '!', '?',
            '"', '\'', '(', ')', '[', ']', '{', '}',
            '-', '_', '/', '\\', '|', '@', '#', '%',
            '*', '+', '=', '<', '>', '~', '`', '^', '&'
        };
        public List<string> GetWords(string text)
        {
            var words = new List<string>();

            if (string.IsNullOrWhiteSpace(text))
                return words;

            string[] tokens = text.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string token in tokens)
            {
                string word = token.ToLower().Trim();

                if (!string.IsNullOrEmpty(word))
                {
                    words.Add(word);
                }
            }

            return words;
        }
    }
}
