using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player_Tilemap : MonoBehaviour
{
    [Header("Main")]
    private Rigidbody2D _rb;

    [Header("Move")]
    [SerializeField] private float _moveSpeed = 3f;
    private Vector2 _moveDirection = Vector2.zero;

    [Header("Jump")]
    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private int _maxJumps = 2;
    private int _usedJumps = 0;

    [Header("CircleCast")]
    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private float _distance = 0.1f;
    [SerializeField] private LayerMask _groundLayer;

    private bool _isGrounded()
    {
        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.CircleCast(origin, _radius, Vector2.down, _distance, _groundLayer);

        if (hit.collider !=  null) return true;
        else return false;
    }

    #region OnInput
    public void OnMoveInput(Vector2 direction)
    {
        _moveDirection = direction;
    }

    public void OnJumpInput(bool isJumping)
    {
        if (!isJumping) return;

        if (_isGrounded()) _usedJumps = 0;

        if (_usedJumps < _maxJumps)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0); // сброс вертикальной скорости
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse); // происходит сложение сил без верхней строчки
            _usedJumps++;
        }
    }
    #endregion

    private void ApplyMovement()
    {
        _rb.AddForce(new Vector2(_moveDirection.x * _moveSpeed, 0), ForceMode2D.Force);
    }

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        ApplyMovement();
    }
}
