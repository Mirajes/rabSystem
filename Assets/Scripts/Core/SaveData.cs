using System;
using UnityEngine.Rendering;

[Serializable]
public class SaveData
{
    public SerializedDictionary<string, string> Data = new();
}
