using System;
using System.IO;
using System.Collections.Generic;

namespace WordCounterV2
{
    public class FileReader
    {
        private string _folderPath;

        public FileReader(string path)
        {
            _folderPath = path;
        }

        public Dictionary<string, string> ReadAllTextFiles()
        {
            Dictionary<string, string> collectedFiles = new Dictionary<string, string>();

            if (!Directory.Exists(_folderPath))
            {
                Console.WriteLine($"[ERROR] Folder does not exist: {_folderPath}");
                return collectedFiles;
            }

            string[] availableFiles = Directory.GetFiles(_folderPath, "*.txt");

            if (availableFiles.Length == 0)
            {
                Console.WriteLine($"[INFO] No .txt documents found in '{_folderPath}'.");
                return collectedFiles;
            }

            foreach (string currentPath in availableFiles)
            {
                try
                {
                    string fileTitle = Path.GetFileName(currentPath);
                    string fileText = File.ReadAllText(currentPath);

                    collectedFiles[fileTitle] = fileText;

                    Console.WriteLine($"Loaded -> {fileTitle}");
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Unable to read: {currentPath}");
                    Console.WriteLine($"Reason: {exception.Message}");
                }
            }

            return collectedFiles;
        }
    }
}