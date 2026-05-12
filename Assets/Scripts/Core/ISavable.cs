public interface ISavable
{
    void OnSave(SaveData data);
    void OnLoad(SaveData data);
}