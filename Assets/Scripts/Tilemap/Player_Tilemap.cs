using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player_Tilemap : MonoBehaviour
{
    [Header("Main")]
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [Header("Move")]
    [SerializeField] private float _moveSpeed = 5f;
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

        Debug.DrawLine(origin, hit.point, Color.yellow, 0.2f);

        if (hit.collider !=  null) return true;
        else return false;
    }

    #region OnInput
    public void OnMoveInput(Vector2 direction)
    {
        _moveDirection = direction;

        if (_moveDirection.x > 0) { _spriteRenderer.flipX = false; _animator.SetBool("Move", true); }
        else if (_moveDirection.x < 0) { _spriteRenderer.flipX = true; _animator.SetBool("Move", true); }
        else _animator.SetBool("Move", false);
    }

    public void OnJumpInput()
    {
        if (_isGrounded()) _usedJumps = 0;

        if (_usedJumps < _maxJumps)
        {
            _animator.SetTrigger("Jump");

            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0); // сброс вертикальной скорости
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse); // происходит сложение сил без верхней строчки
            _usedJumps++;
        }
    }
    #endregion

    private void ApplyMovement()
    {
        if (_moveDirection.x == 0) return;

        _rb.AddForceX(_moveDirection.x * _moveSpeed, ForceMode2D.Force);

        if (math.abs(_rb.linearVelocityX) > _moveSpeed)
            _rb.linearVelocityX = _rb.linearVelocity.normalized.x * _moveSpeed;

        //Vector2 velocity = _rb.linearVelocity;
        //if (math.abs(velocity.x) > _moveSpeed)
        //    _rb.linearVelocity = new Vector2(velocity.x, velocity.y);
            //velocity.x = math.sign(velocity.x) * _moveSpeed;


        //_rb.AddForce(new Vector2(_moveDirection.x * _moveSpeed, 0), ForceMode2D.Force);
        //if (_moveDirection.x == 0) { _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y); return; }
        //Vector2 velocity = _rb.linearVelocity;
        //if (math.abs(velocity.x) > _moveSpeed) 
        //    velocity.x = math.sign(velocity.x) * _moveSpeed;
        //_rb.linearVelocity = new Vector2(velocity.x, velocity.y);

    }

    private void KnowVelocityY()
    {
        _animator.SetFloat("Vertical", _rb.linearVelocityY);
    }

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _groundLayer = LayerMask.GetMask("Ground");

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ApplyMovement();

        KnowVelocityY();
    }
}
