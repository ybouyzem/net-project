using System;
using System.Collections.Generic;

namespace WordCounterV2
{
    class Program
    {
        static void Main(string[] arguments)
        {
            ShowHeader();

            string folderPath = RequestFolderPath(arguments);

            if (string.IsNullOrEmpty(folderPath))
            {
                Console.WriteLine("[ERROR] No valid folder path provided.");
                return;
            }

            int maxLength = ReadIntegerValue(
                "Enter max word length (N) [Default: 5]: ",
                5,
                arguments.Length > 1 ? arguments[1] : string.Empty);

            int trimCount = ReadIntegerValue(
                "Enter number of ending chars to remove (M) [Default: 2]: ",
                2,
                arguments.Length > 2 ? arguments[2] : string.Empty);

            PrintConfiguration(folderPath, maxLength, trimCount);

            FileReader textReader = new FileReader(folderPath);
            Dictionary<string, string> loadedTexts = textReader.ReadAllTextFiles();

            if (loadedTexts.Count == 0)
            {
                Console.WriteLine("\n[INFO] No readable text files found.");
                return;
            }

            Console.WriteLine($"\n[OK] Loaded {loadedTexts.Count} file(s).\n");

            TextProcessor wordProcessor = new TextProcessor(maxLength, trimCount);
            WordCounter statistics = new WordCounter();

            foreach (KeyValuePair<string, string> item in loadedTexts)
            {
                Console.WriteLine($"Processing file -> {item.Key}");

                List<string> extractedWords = wordProcessor.GetWords(item.Value);
                statistics.AddWords(extractedWords);
            }

            Console.WriteLine("\nFinished analyzing files.\n");

            statistics.DisplayResults();

            Console.WriteLine("\nPress ENTER to exit...");
            Console.ReadLine();
        }

        private static void ShowHeader()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("         TEXT WORD ANALYZER V2");
            Console.WriteLine("=========================================\n");
        }

        private static string RequestFolderPath(string[] arguments)
        {
            if (arguments.Length > 0)
            {
                return arguments[0];
            }

            Console.Write("Enter folder location: ");

            string? userInput = Console.ReadLine();

            return userInput?.Trim() ?? string.Empty;
        }

        private static void PrintConfiguration(string folder, int nValue, int mValue)
        {
            Console.WriteLine("\n============= SETTINGS =================");
            Console.WriteLine($"Directory     : {folder}");
            Console.WriteLine($"N Parameter   : {nValue}");
            Console.WriteLine($"M Parameter   : {mValue}");
            Console.WriteLine($"Rule Applied  : Trim {mValue} char(s) if word > {nValue}");
            Console.WriteLine("========================================");
        }

        private static int ReadIntegerValue(
            string message,
            int fallbackValue,
            string? argumentValue = null)
        {
            if (!string.IsNullOrWhiteSpace(argumentValue) &&
                int.TryParse(argumentValue, out int parsedValue) &&
                parsedValue > 0)
            {
                return parsedValue;
            }

            Console.Write(message);

            string? inputText = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(inputText))
            {
                return fallbackValue;
            }

            if (int.TryParse(inputText.Trim(), out int finalValue) &&
                finalValue > 0)
            {
                return finalValue;
            }

            Console.WriteLine($"[WARNING] Invalid value. Using default ({fallbackValue}).");

            return fallbackValue;
        }
    }
}