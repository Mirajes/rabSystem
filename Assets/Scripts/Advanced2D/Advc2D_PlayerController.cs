using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Advc2D_PlayerController : MonoBehaviour
{
    private CharacterController _charController;

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
        _charController.Move(_moveDirection);
    }

    private void OnEnable()
    {
        _charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyMovement();
    }
}
