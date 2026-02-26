using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Advc2D_PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    [Header("Settings")]
    [SerializeField] private int _currentHealth = 1;
    [SerializeField] private float _moveSpeed = 3f;
    
    private Vector2 _moveDirection = Vector2.zero;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        _moveDirection = direction;
    }

    private void ApplyMovement()
    {
        _rigidBody.MovePosition(_rigidBody.position + (_moveDirection * _moveSpeed * Time.deltaTime));
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        float angle = Vector2.Angle(Vector2.zero, transform.forward);
        
    }

    private void OnEnable()
    {
    }

    private void Update()
    {
        ApplyMovement();
    }
}
