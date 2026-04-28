using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PB_PlayerController : MonoBehaviour
{
    private CharacterController _characterController;

    [Header("WASD")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeed;
    private Vector3 _moveDirection;

    [Header("JUMP")]
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _velocity_Y;
    public bool IsGrounded => _characterController.isGrounded;


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        _moveDirection.x = moveInput.x;
        _moveDirection.z = moveInput.y;
    }
}
