using UnityEngine;

public class Advc2D_ChasingEnemy : Advc2D_Enemy 
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _target;
    
    private void ApplyMove()
    {
        Vector2 direction = (transform.position - _target.position); // можно нормализовать чтобы была постоянная скорость

        _RigidBody.MovePosition(_RigidBody.position - (direction * _moveSpeed * Time.fixedDeltaTime));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        _target = collision.transform;
        ApplyMove();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        _target = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        GM_Advanced2D.PlayerHit?.Invoke();
    }
}