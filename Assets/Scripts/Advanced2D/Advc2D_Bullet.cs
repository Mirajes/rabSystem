using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Advc2D_Bullet : MonoBehaviour
{
    public Rigidbody2D Rigidbody => _rigidBody;

    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _timeToDisappear = 10f;

    private Rigidbody2D _rigidBody;

    public void SpawnBullet(Vector2 direction)
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        print(direction);
        _rigidBody.AddForce(direction * _moveSpeed, ForceMode2D.Impulse);

        DestroyBulletAsync().Forget();
    }

    async UniTask DestroyBulletAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_moveSpeed));
        Destroy(this.gameObject);
    }
}
