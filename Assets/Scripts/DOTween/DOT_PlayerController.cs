using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class DOT_PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _moveDistanceMax = 10f;

    [Header("Rotate")]
    [SerializeField] private float _rotateSpeed = 15f;
    [SerializeField] private float _rotateMax = 90f;

    [Header("Jump")]
    [SerializeField] private float _jumpPower = 3f;
    [SerializeField] private float _jumpPowerMax = 7f;

    private float _rotateDirection = 0f;
    public ref float RotateDirection => ref _rotateDirection;

    private float _walkDirection = 0f;
    public ref float WalkDirection => ref _walkDirection;

    private Rigidbody _rb;
    private Transform _playerTransform;
    private Vector2 _moveDirection;
    private float _rotateAmount;
    private GM_Doom _gameManager;

    #region Inputs

    public void OnActionChoose(Actions action)
    {
        if (action == Actions.Move && _walkDirection > 0f)
            _gameManager.OnInputAction(3, _moveDistanceMax / _moveSpeed);
        else if (action == Actions.Move && _walkDirection < 0f)
            _gameManager.OnInputAction(1, _moveDistanceMax / _moveSpeed);
        else if (action == Actions.Jump)
            _gameManager.OnInputAction(2, _jumpPowerMax / _jumpPower);
        else if (action == Actions.Rotate && _rotateDirection > 0f)
            _gameManager.OnInputAction(4, _rotateMax / _rotateSpeed);
        else if (action == Actions.Rotate && _rotateDirection < 0f)
            _gameManager.OnInputAction(0, _rotateMax / _rotateSpeed);
    }

    public void OnActionStop(Actions action)
    {
        if (action == Actions.Move)
        {
            _gameManager.OnInputCancelled(3);
            _gameManager.OnInputCancelled(1);
        }
        else if (action == Actions.Jump)
            _gameManager.OnInputCancelled(2);
        else if (action == Actions.Rotate)
        {
            _gameManager.OnInputCancelled(4);
            _gameManager.OnInputCancelled(0);
        }
    }

    public void OnWalkDirectionChoose(float input, ref float controller)
    {
        controller = input;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (!_gameManager.IsPlaying) return;

        float chargingTime = _moveSpeed * (float)context.duration;

        _moveDirection.x = chargingTime * _walkDirection;

        if (Mathf.Abs(_moveDirection.x) > _moveDistanceMax)
            _moveDirection.x = _moveDistanceMax * _walkDirection;

        _playerTransform.DOMove(_playerTransform.position + _playerTransform.forward * _moveDirection.x, 1);
    }

    public void OnRotateInput(InputAction.CallbackContext context)
    {
        if (!_gameManager.IsPlaying) return;

        _rotateAmount = _rotateSpeed * (float)context.duration * _rotateDirection;

        if (Mathf.Abs(_rotateAmount) > _rotateMax)
            _rotateAmount = _rotateMax * context.ReadValue<float>();

        _playerTransform.DORotate(new Vector3(0, _playerTransform.eulerAngles.y + _rotateAmount, 0), 1);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (!_gameManager.IsPlaying) return;

        _moveDirection.y = _jumpPower * (float)context.duration;

        if (_moveDirection.y > _jumpPowerMax) _moveDirection.y = _jumpPowerMax;

        _playerTransform.DOJump(_playerTransform.position + _playerTransform.forward * _moveDirection.y, _jumpPower, 1, 1);
    }
    #endregion

    private void OnEnable()
    {
        _gameManager = FindAnyObjectByType<GM_Doom>();
        _rb = GetComponent<Rigidbody>();
        _playerTransform = this.transform;
    }
}