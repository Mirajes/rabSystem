using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Advc2D_Box : Advc2D_IntetactableObject
{
    [SerializeField] private float _hp = math.INFINITY;
    [SerializeField] private bool _isMovable = false;

    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Rigidbody2D _rigidBody;

    private void OnEnable()
    {
        if (!_isMovable) _rigidBody.bodyType = RigidbodyType2D.Static;
        else _rigidBody.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _hp -= 1;
            CheckHP();
        }
    }

    private void CheckHP() 
    {
        if (_hp <= 0)            
        {
            Destroy(this.gameObject);
        }
        
    }
}