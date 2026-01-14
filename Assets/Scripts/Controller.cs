using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Controller : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;

    private CharacterController _charController;
    private Vector3 _moveDirection;

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        _moveDirection = new Vector3(direction.x, _moveDirection.y, direction.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _moveDirection.y = _jumpPower;
    }

    private void ApplyMovement()
    {
        _charController.Move(_moveDirection * Time.deltaTime * _moveSpeed);
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
