using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Controller Settings")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _gravityMult = 1f;

    [SerializeField] private float _velocity_Y = 0f;
    private Vector3 _moveDirection;
    public bool IsGrounded => _charController.isGrounded;

    [Header("Camera Settings")]
    [SerializeField] private float _mouseSens = 1f;
    [SerializeField] private float _scrollSpeed = 0.2f;
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, 1f, 0f);
    [SerializeField] private Vector3 _zoomLimitsMax_YZ = new Vector3(0f, 3f, -3f);
    
    private Vector2 _currentZoom_YZ;
    private Vector2 _cameraRotation;
    private float _cameraVerticalAngle = 0f;

    [Header("Main")]
    private CharacterController _charController;
    private Camera _camera;

    public bool _isInsideCar = false;

    #region Inputs
    public void OnMoveInput(Vector2 input)
    {
        _moveDirection = new Vector3(input.x, _moveDirection.y, input.y);
    }

    public void OnJumpInput()
    {
        if (IsGrounded)
            _velocity_Y += _jumpPower;
    }

    public void OnRotateInput(Vector2 input)
    {
        _cameraRotation = new Vector2(input.x, input.y);
    }

    public void OnZoomInput(Vector2 input)
    {
        _currentZoom_YZ += new Vector2(-input.y, input.y) * _scrollSpeed;

        if (_currentZoom_YZ.x >= _zoomLimitsMax_YZ.y)
            _currentZoom_YZ.x = _zoomLimitsMax_YZ.y;
        else if (_currentZoom_YZ.x <= _cameraOffset.y)
            _currentZoom_YZ.x = _cameraOffset.y;

        if (_currentZoom_YZ.y <= _zoomLimitsMax_YZ.z)
            _currentZoom_YZ.y = _zoomLimitsMax_YZ.z;
        else if (_currentZoom_YZ.y >= _cameraOffset.z)
            _currentZoom_YZ.y = -_cameraOffset.z;

            _camera.transform.localPosition = new Vector3(0f, _currentZoom_YZ.x, _currentZoom_YZ.y);
    }

    public void OnCarSummonInput()
    {
        GameManager gm = FindAnyObjectByType<GameManager>();

        gm.CarSummon?.Invoke();
    }
    #endregion

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
        Vector3 move = this.transform.right * _moveDirection.x + this.transform.up * _moveDirection.y + this.transform.forward * _moveDirection.z;
        _charController.Move(move * _moveSpeed * Time.deltaTime);
    }

    private void ApplyRotation()
    {
        float mouseX = _cameraRotation.x * _mouseSens;
        float mouseY = _cameraRotation.y * _mouseSens;

        _cameraVerticalAngle -= mouseY;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -90f, 90f); // для машины переделать

        _camera.transform.localRotation = Quaternion.Euler(_cameraVerticalAngle, 0, 0);

        this.transform.Rotate(Vector3.up * mouseX);
    }

    private void CheckForInteract() // todo
    {
        throw new NotImplementedException();
    }

    private void OnEnable()
    {
        _camera = Camera.main;

        _camera.transform.parent = this.transform;
        _camera.transform.localPosition = _cameraOffset;

        _zoomLimitsMax_YZ += _cameraOffset;
        _currentZoom_YZ = _cameraOffset;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        _charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyRotation();

        SetGravity();
        ApplyMovement();
    }
}
