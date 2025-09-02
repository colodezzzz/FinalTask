using System.IO;

namespace FinalTask.SaveLoadService
{
    class FileSystemSaveLoadService : ISaveLoadService<string>
    {
        private const string DEFAULT_FILES_EXTENSION = ".txt";

        private string _dataPath;

        public FileSystemSaveLoadService(string path)
        {
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            _dataPath = path;
        }

        public string LoadData(string id)
        {
            string path = Path.Combine(_dataPath, GetFileName(id));

            if (File.Exists(path))
            {
                string result;
                StreamReader readStream = File.OpenText(path);

                try
                {
                    result = readStream.ReadToEnd();
                }
                finally
                {
                    readStream.Close();
                    readStream.Dispose();
                }

                return result;
            }

            return null;
        }

        public void SaveData(string data, string id)
        {
            string path = Path.Combine(_dataPath, GetFileName(id));

            if (File.Exists(path) == false)
            {
                FileStream fileStream = File.Create(path);
                fileStream.Close();
                fileStream.Dispose();
            }

            StreamWriter writeStream = File.CreateText(path);

            try
            {
                writeStream.Write(data);
            }
            finally
            {
                writeStream.Close();
                writeStream.Dispose();
            }
        }

        private string GetFileName(string fileName)
        {
            return fileName + DEFAULT_FILES_EXTENSION;
        }
    }
}
