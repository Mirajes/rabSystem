using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(BoxCollider))]
public class Advc3D_PlayerController : MonoBehaviour 
{
    private CharacterController _charController;

    [Header("Walk")]
    [SerializeField] private float _moveSpeed = 3f;
    private Vector2 _moveDirection;

    [Header("Rotate")]
    [SerializeField] private float _rotateSpeed = 15f;
    private float _rotateDirection = 0f;

    [Header("Jump")]
    [SerializeField] private float _jumpPower = 10f;
    [SerializeField] private int _maxJumps = 1;
    private int _usedJumps = 0;
    public bool IsGrounded => _charController.isGrounded;

    [Header("Gravity")]
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _gravityMult = 1f;

    [Header("Grab")]
    [SerializeField] private float _interactDistance = 2f;
    [SerializeField] private bool _isHandFull = false;
    [SerializeField] private GameObject _grabbedObj;
    [SerializeField] private Vector3 _grabOffset = new Vector3(0f, 2f, 2f);

    [Header("Visual")]
    [SerializeField] private Renderer _renderer;
    public Renderer Renderer => _renderer;

    private void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        SetGravity();

        ApplyRotation();
        ApplyMovement();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (_isHandFull)
        {
            ReleaseGrab();
            _isHandFull = false;
        }
        else
        {
            CheckForInteract();
        }
    }

    public void OnControllInput(InputAction.CallbackContext context)
    {
        Vector2 controllDirection = context.ReadValue<Vector2>();

        _moveDirection.x = controllDirection.y * _moveSpeed;
        _rotateDirection = controllDirection.x * _rotateSpeed;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (_usedJumps >= _maxJumps) return;

        _usedJumps++;
        _moveDirection.y = _jumpPower;
    }

    private void SetGravity()
    {
        if (IsGrounded && _moveDirection.y < 0)
        {
            _moveDirection.y = -1f;
            _usedJumps = 0;
        }
        else
        {
            _moveDirection.y += _gravity * _gravityMult * Time.deltaTime;
        }
    }

    private void ApplyMovement()
    {
        Vector3 move = (transform.forward * _moveDirection.x + transform.up * _moveDirection.y) * Time.deltaTime;
        _charController.Move(move);
    }

    private void ApplyRotation()
    {
        Vector3 rotate = (transform.up * _rotateDirection * Time.deltaTime);
        transform.Rotate(rotate);
    }

    private void CheckForInteract()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactDistance))
        {
            var grabbable = hit.collider.GetComponent<IGrabbable>();
            if (grabbable != null)
            {
                _grabbedObj = hit.collider.gameObject;
                _grabbedObj.transform.parent = this.transform;
                _grabbedObj.transform.localPosition = _grabOffset;
                _grabbedObj.transform.localRotation = Quaternion.identity;

                grabbable.OnGrab();
                _isHandFull = true;
            }
        }

        if (Physics.Raycast(ray, out hit, _interactDistance))
        {
            var readable = hit.collider.GetComponent<IReadable>();
            if (readable != null)
            {
                readable.OnRead();
            }
        }
    }

    private void ReleaseGrab()
    {
        var grabbed = _grabbedObj.GetComponent<IGrabbable>();
        if (grabbed != null)
            grabbed.OnRelease();

        _grabbedObj = null;
    }
}
