namespace FinalTask.SaveLoadService
{
    public interface ISaveLoadService
    {
        void SaveData<T>(T data, string id);
        T LoadData<T>(string id);
    }
}
