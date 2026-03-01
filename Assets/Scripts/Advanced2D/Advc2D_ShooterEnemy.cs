using UnityEngine;

public class Advc2D_ShooterEnemy : Advc2D_Enemy
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _bulletSpeed;

    [SerializeField] private Advc2D_BulletEnemy _bulletPrefab;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        _target = collision.transform;
        Attack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    protected override void Attack()
    {
        if (_target == null) return;

        Vector2 direction = transform.position - _target.position;

        Advc2D_BulletEnemy newBullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        newBullet.SpawnBullet(direction.normalized, _bulletSpeed);
    }
}