using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Advc3D_AnvilSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private GameObject _anvilPrefab;
    [SerializeField] private float _cd = 3f;
    private bool _debounce;

    private CancellationTokenSource _cts;

    private void SpawnAnvil()
    {
        Advc3D_Level.SpawnObjectInsideLevel?.Invoke(_anvilPrefab.transform, _spawnPos);
    }

    private async UniTask DelaySpawner(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        await UniTask.Delay(TimeSpan.FromSeconds(_cd));
        _debounce = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !_debounce)
        {
            _debounce = true;
            _cts = new();

            SpawnAnvil();
            DelaySpawner(_cts.Token);
        }
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }
}