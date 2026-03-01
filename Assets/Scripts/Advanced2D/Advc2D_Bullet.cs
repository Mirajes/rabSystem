using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Advc2D_Bullet : MonoBehaviour
{
    public Rigidbody2D Rigidbody => _rigidBody;

    [SerializeField] private float _timeToDisappear = 10f;

    private Rigidbody2D _rigidBody;
    private CancellationTokenSource _cts;

    public void SpawnBullet(Vector2 direction, float bulletForce)
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _cts = new CancellationTokenSource();

        _rigidBody.AddForce(direction * bulletForce, ForceMode2D.Impulse);

        DestroyBulletAsync(_cts.Token).Forget();
    }

    async UniTask DestroyBulletAsync(CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_timeToDisappear), cancellationToken: token);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        _cts.Cancel();
        _cts.Dispose();
    }
}
