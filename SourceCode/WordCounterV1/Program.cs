using System;
using System.Collections.Generic;

namespace WordCounterV1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   Word Frequency Counter - Version 1   ");
            Console.WriteLine("========================================");
            Console.WriteLine();

            string directoryPath;
            if (args.Length > 0)
            {
                directoryPath = args[0];
            }
            else
            {
                Console.Write("Enter the path to the folder containing .txt files: ");
                directoryPath = Console.ReadLine()?.Trim() ?? "";
            }

            if (string.IsNullOrEmpty(directoryPath))
            {
                Console.WriteLine("Error: No directory path provided.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Reading files from: {directoryPath}");
            Console.WriteLine("----------------------------------------");

            FileReader reader = new FileReader(directoryPath);
            Dictionary<string, string> fileContents = reader.ReadAllTextFiles();

            if (fileContents.Count == 0)
            {
                Console.WriteLine("No files were read. Exiting.");
                return;
            }

            Console.WriteLine($"\nSuccessfully read {fileContents.Count} file(s).");
            Console.WriteLine("----------------------------------------");

            TextProcessor processor = new TextProcessor();
            WordCounter counter = new WordCounter();

            foreach (KeyValuePair<string, string> file in fileContents)
            {
                Console.WriteLine($"Processing: {file.Key}");
                var words = processor.GetWords(file.Value);
                counter.AddWords(words);
            }

            counter.DisplayResults();

            Console.WriteLine();
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
