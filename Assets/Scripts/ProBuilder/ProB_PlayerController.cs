using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ProB_PlayerController : MonoBehaviour
{
    [Header("CORE")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Camera _camera;
    private CancellationTokenSource _cts;

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
    [SerializeField] private float _dashSpeed = 0.2f;
    private bool _isDashing = false;
    private Vector2 _dashVelocity;

    [Header("LOOK")]
    [SerializeField] private float _mouseSensivity;
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, 1f, 0f);
    private Vector2 _cameraInput;
    private float _cameraVerticalAngle = 0f;

    [Header("FUNNY")]
    [SerializeField] private A_ItemTool_ProB _currentTool;

    private void Start()
    {
        _cts = new();

        _camera.transform.localPosition = _cameraOffset;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleRotation();
        SetGravity();

        HandleJump();
        HandleMove();

        Vector3 screenPos = Input.mousePosition;
        Vector3 screenToWorld = _camera.ScreenToWorldPoint(screenPos);
        Debug.DrawRay(screenToWorld, _camera.transform.forward * 100);
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    #region Inputs
    public void OnLookInput(InputAction.CallbackContext context)
    {
        _cameraInput = context.ReadValue<Vector2>();
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
        if (context.started && !_isDashing)
        {
            _isDashing = true;
            _dashVelocity = _moveInput * _dashDistance;
            DashTask(_cts.Token).Forget();
        }
    }

    public void OnMainClickInput(InputAction.CallbackContext context) 
    {
        _currentTool.MainActivate(); 
    }

    public void OnSecondaryClickInput(InputAction.CallbackContext context) 
    {
        _currentTool.SecondaryActivate();
    }
    #endregion

    #region Handle
    private void HandleRotation()
    {
        float mouseX = _cameraInput.x * _mouseSensivity;
        float mouseY = _cameraInput.y * _mouseSensivity;

        _cameraVerticalAngle -= mouseY;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -90f, 90f);

        _camera.transform.localRotation = Quaternion.Euler(_cameraVerticalAngle, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void SetGravity()
    {
        if (_isDashing) { _velocity_Y = 0f; return; }

        if (IsGrounded && !_isJumping)
        {
            _velocity_Y = -1;
        }
        else
        {
            _velocity_Y -= _gravity * Time.deltaTime;
        }
    }

    private void HandleMove()
    {
        _moveVelocity = new Vector3(
            _moveInput.x * _moveSpeed,
            _velocity_Y,
            _moveInput.y * _moveSpeed);

        if (_isDashing)
        {
            _moveVelocity += new Vector3(_dashVelocity.x, 0f, _dashVelocity.y);
        }

        Vector3 move = (
            transform.right * _moveVelocity.x
            + transform.up * _moveVelocity.y
            + transform.forward * _moveVelocity.z
            );

        _characterController.Move(move * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (_isJumping && IsGrounded)
        {
            _velocity_Y = _jumpPower;
        }
    }

    private async UniTask DashTask(CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_dashSpeed), cancellationToken: token);

        _isDashing = false;
        _dashVelocity = Vector2.zero;
    }
    #endregion
}
