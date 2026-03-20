using System;
using System.Collections.Generic;
using UnityEngine;

// в идеале хранить тут все предметы и их положения относительно объекта чтобы не пересоздавать
// его заного в GM
public class Advc3D_Level : MonoBehaviour 
{
    public Transform SpawnPos => _spawnPos;
    public List<Advc3D_Coin> Coins => _coins;
    public List<Transform> CameraPoses => _cameraPoses;

    [SerializeField] private Transform _spawnPos;
    [SerializeField] private List<Advc3D_Coin> _coins;
    [SerializeField] private List<Transform> _cameraPoses;

    public static Action<Transform, Transform> SpawnObjectInsideLevel;

    private void OnSpawnObject(Transform obj, Transform spawnPos)
    {
        Instantiate(obj, spawnPos.position, spawnPos.rotation, this.transform);
    }

    private void OnEnable()
    {
        SpawnObjectInsideLevel += OnSpawnObject;
    }

    private void OnDisable()
    {
        SpawnObjectInsideLevel -= OnSpawnObject;
    }
}