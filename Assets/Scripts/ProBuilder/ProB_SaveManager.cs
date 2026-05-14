using System;
using UnityEngine;

public class ProB_SaveManager
{
    public static Action<SaveData> OnSave;
    public static Action<SaveData> OnLoad;

    public void Save()
    {
        SaveData data = new SaveData();
        OnSave?.Invoke(data);

        string save = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("Save", save);
        PlayerPrefs.Save();
    }

    // vidimo on kazhdui raz perezapisivaet staroe sohranenie vnezavisimosti ot togo
    // est' li izmeneniya ili net => optimization?
    public void Load()
    {
        var json = PlayerPrefs.GetString("Save", "");
        if (string.IsNullOrEmpty(json))
            return;

        SaveData data = new SaveData();
        data = JsonUtility.FromJson<SaveData>(json);

        OnLoad?.Invoke(data);
    }
}

/*
sohranyaem vragov vo vremya zakritiya UI => klass :)
eshe i perezapisivaem absolute vse :)))
*/ 