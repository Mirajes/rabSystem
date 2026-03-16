using UnityEngine;

// в идеале хранить тут все предметы и их положения относительно объекта чтобы не пересоздавать
// его заного в GM
public class Advc3D_Level : MonoBehaviour 
{
    public Transform SpawnPos => _spawnPos;
    [SerializeField] private Transform _spawnPos;
}