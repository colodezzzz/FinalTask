using System;
using System.IO;

namespace FinalTask.SaveLoadService
{
    class FileSystemSaveLoadService : ISaveLoadService<string>
    {
        private const string DEFAULT_FILES_EXTENSION = ".txt";

        private string _dataPath;

        public FileSystemSaveLoadService(string path)
        {
            _dataPath = path;
        }

        public string LoadData(string id)
        {
            string path = Path.Combine(_dataPath, GetFileName(id));

            if (File.Exists(path))
            {
                using (StreamReader readStream = File.OpenText(path))
                {
                    string result = readStream.ReadToEnd();
                    return result;
                }
            }
            else
            {
                Console.WriteLine("Can't load data! File doesn't exist!");
            }

            return null;
        }

        public void SaveData(string data, string id)
        {
            string path = Path.Combine(_dataPath, GetFileName(id));

            if (File.Exists(path) == false)
            {
                File.Create(path);
            }

            using (StreamWriter writeStream = File.CreateText(path))
            {
                writeStream.WriteLine(data);
            }
        }

        private string GetFileName(string fileName)
        {
            return fileName + DEFAULT_FILES_EXTENSION;
        }
    }
}
