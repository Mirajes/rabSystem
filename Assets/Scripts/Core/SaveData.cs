using System;
using UnityEngine.Rendering;

[Serializable]
public class SaveData
{
    // ParametrName + Value
    public SerializedDictionary<string, string> Data = new();
}