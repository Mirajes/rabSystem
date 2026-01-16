using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Controller : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _gravityMult = 1f;

    [SerializeField] private float _velocity_Y = 0f;

    private Vector3 _moveDirection;

    [Header("Main")]
    private CharacterController _charController;
    private Camera _camera;

    public void OnMove(Vector2 direction)
    {
        _moveDirection = new Vector3(direction.x, _moveDirection.y, direction.y);
    }

    public void OnJump()
    {
        if (IsGrounded)
            _velocity_Y += _jumpPower;
    }

    public void OnRotate()
    {

    }

    public void OnZoom()
    {

    }

    private void SetGravity()
    {
        if (IsGrounded && _velocity_Y < 0f)
            _velocity_Y = -1f;
        else
            _velocity_Y += _gravity * _gravityMult * Time.deltaTime;

        _moveDirection.y = _velocity_Y;
    }

    private void ApplyMovement()
    {
        _charController.Move(_moveDirection * Time.deltaTime * _moveSpeed);
    }

    private void ApplyRotation()
    {

    }

    private void OnEnable()
    {
        _charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        SetGravity();
        ApplyMovement();
    }

    public bool IsGrounded => _charController.isGrounded;
}
