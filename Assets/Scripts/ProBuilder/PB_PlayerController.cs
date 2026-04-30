using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PB_PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;

    [Header("WASD")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeed;
    private Vector2 _moveInput;
    private Vector3 _moveVelocity;

    [Header("JUMP")]
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private float _velocity_Y;
    private bool _isJumping = false;
    public bool IsGrounded => _characterController.isGrounded;

    [Header("DASH")]
    [SerializeField] private float _dashDistance;
    private Vector2 _dashVelocity;

    private void Update()
    {
        HandleJump();
        SetGravity();

        HandleMove();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        _isJumping = context.ReadValueAsButton();
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        _dashVelocity = _moveInput * _dashDistance;
    }

    private void HandleMove()
    {
        _moveVelocity = new Vector3(
            _moveInput.x * _moveSpeed,
            _velocity_Y,
            _moveInput.y * _moveSpeed)
            + new Vector3(_dashVelocity.x, 0, _dashVelocity.y);
        _characterController.Move(_moveVelocity * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (_isJumping && IsGrounded)
        {
            _velocity_Y = _jumpPower;
        }
    }

    private void SetGravity()
    {
        if (IsGrounded && !_isJumping)
        {
            _velocity_Y = -1;
        }
        else
        {
            _velocity_Y -= _gravity * Time.deltaTime;
        }
    }
}
