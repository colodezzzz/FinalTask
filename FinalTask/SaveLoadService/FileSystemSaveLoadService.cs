using System;
using System.IO;

namespace FinalTask.SaveLoadService
{
    class FileSystemSaveLoadService : ISaveLoadService
    {
        private const string DEFAULT_FILES_EXTENSION = ".txt";

        private string _dataPath;

        public FileSystemSaveLoadService(string path)
        {
            _dataPath = path;
        }

        public T LoadData<T>(string id)
        {
            string path = Path.Combine(_dataPath, GetFileName(id));

            if (File.Exists(path))
            {
                using (StreamReader readStream = File.OpenText(path))
                {
                    string result = readStream.ReadToEnd();

                    
                }
            }
            else
            {
                Console.WriteLine("Can't load data! File doesn't exists!");
            }

            return default;
        }

        public void SaveData<T>(T data, string id)
        {
            string path = Path.Combine(_dataPath, GetFileName(id));

            if (File.Exists(path) == false)
            {
                File.Create(path);
            }

            using (StreamWriter writeStream = File.CreateText(path))
            {
                writeStream.WriteLine(data.ToString());
            }
        }

        private string GetFileName(string fileName)
        {
            return fileName + DEFAULT_FILES_EXTENSION;
        }
    }
}
