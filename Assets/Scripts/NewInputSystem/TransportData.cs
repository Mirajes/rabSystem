using UnityEngine;

[CreateAssetMenu(fileName = "TransportData", menuName = "Scriptable Objects/TransportData")]
public class TransportData : ScriptableObject
{
    public string Name;
    public float Speed;
    public float Weight;

    public Vector3 CameraOffset;
}
