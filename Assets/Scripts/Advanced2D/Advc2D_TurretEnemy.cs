using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class Advc2D_TurretEnemy : Advc2D_Enemy
{
    [SerializeField] private Advc2D_Bullet _bullet;
    [SerializeField] private float _shootDelay = 3f;

    [SerializeField] private Vector3 _shootOffset = new Vector3(0, 0.3f);

    CancellationTokenSource _cancellationToken;

    UniTask _shootTask;

    protected override void Attack()
    {
        base.Attack();

        Advc2D_Bullet newBullet = Instantiate(_bullet, transform.position + (_shootOffset.y * transform.up), transform.rotation);
        newBullet.SpawnBullet(transform.up);
    }

    private async UniTask ShootingLoop()
    {
        try
        {
            while (!_cancellationToken.Token.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_shootDelay), cancellationToken: _cancellationToken.Token);
                Attack();
            }
        }
        catch (Exception)
        {
            Console.WriteLine("галя Отмена");
            throw;
        }
    }

    private void Start()
    {
        _bullet = Resources.Load<Advc2D_Bullet>("KT_Advc2D/Bullet");
    }

    private void OnEnable()
    {
        _cancellationToken = new();
        ShootingLoop().Forget();
    }

    private void OnDisable()
    {
        _cancellationToken.Cancel();
        _cancellationToken.Dispose();
    }
}