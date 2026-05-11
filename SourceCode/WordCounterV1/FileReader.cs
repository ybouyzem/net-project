using System;
using System.IO;
using System.Collections.Generic;

namespace WordCounterV1
{
    public class FileReader
    {
        private string _folderLocation;

        public FileReader(string folderPath)
        {
            _folderLocation = folderPath;
        }

        public Dictionary<string, string> ReadAllTextFiles()
        {
            Dictionary<string, string> loadedFiles = new Dictionary<string, string>();

            if (!Directory.Exists(_folderLocation))
            {
                Console.WriteLine($"[ERROR] Folder not found: {_folderLocation}");
                return loadedFiles;
            }

            string[] textFileList = Directory.GetFiles(_folderLocation, "*.txt");

            if (textFileList.Length == 0)
            {
                Console.WriteLine($"[INFO] No text files detected in: {_folderLocation}");
                return loadedFiles;
            }

            foreach (string currentFile in textFileList)
            {
                try
                {
                    string shortName = Path.GetFileName(currentFile);
                    string textData = File.ReadAllText(currentFile);

                    loadedFiles.Add(shortName, textData);

                    Console.WriteLine($"-> Successfully loaded: {shortName}");
                }
                catch (Exception error)
                {
                    Console.WriteLine($"-> Failed to load file: {currentFile}");
                    Console.WriteLine($"   Reason: {error.Message}");
                }
            }

            return loadedFiles;
        }
    }
}