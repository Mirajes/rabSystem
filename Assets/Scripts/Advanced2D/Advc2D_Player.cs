using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Advc2D_Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _currentHealth = 1;
    [SerializeField] private bool _isShielded = false;
    [SerializeField] private float _shieldSize = 1.3f;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _shieldExplosionRange = 3.5f;
    [SerializeField] private bool _isArmed = false;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Vector3 _shootOffset = new Vector3(0f, 0.7f, 0f);

    [Header("cool")]
    [SerializeField] private SpriteRenderer _cannon;
    [SerializeField] private Advc2D_BulletPlayer _bulletPrefab;
    [SerializeField] private CircleCollider2D _shieldCol;
    [SerializeField] private Advc2D_Shield _shield;

    private Vector2 _mousePos;

    public int CurrentHealth => _currentHealth;
    public bool IsShielded => _isShielded;

    private Vector2 _moveDirection = Vector2.zero;
    private Rigidbody2D _rigidBody;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        _moveDirection = direction;

        transform.eulerAngles = _moveDirection * 180f;
    }

    public void OnMouseInput(InputAction.CallbackContext context)
    {
        if (!_isArmed) return;

        _mousePos = context.ReadValue<Vector2>();
        Vector3 lookAt = Camera.main.ScreenToWorldPoint( _mousePos );

        Vector3 direction = _cannon.transform.position - lookAt;

        _cannon.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    public void OnShootInput(InputAction.CallbackContext context)
    {
        if (!_isArmed) return;

        Advc2D_Bullet newBullet = Instantiate(_bulletPrefab, transform.position, transform.rotation); // fix

        Vector3 direction = this.transform.position - Camera.main.ScreenToWorldPoint( _mousePos );

        newBullet.SpawnBullet(-direction, _bulletSpeed);
    }

    private void ApplyMovement()
    {
        _rigidBody.MovePosition(_rigidBody.position + (_moveDirection * _moveSpeed * Time.fixedDeltaTime));
    }


    public void CreateShield()
    {
        _isShielded = true;
        _shieldCol.transform.localScale = Vector3.one;
        _shield.gameObject.SetActive(true);
    }

    public void DestroyShield()
    {
        _isShielded = false;
        _shieldCol.transform.localScale *= _shieldExplosionRange;
        _shield.SetExploding(true);
    }

    public void SetGun(bool isArmed)
    {
        _isArmed = isArmed;
        GunInteract();
    }

    public void GunInteract()
    {
        if (_isArmed) _cannon.gameObject.SetActive(true);
        else _cannon.gameObject.SetActive(false);
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        GunInteract();
    }

    private void FixedUpdate() // fixed update так как rigidBody обрабатывается тут
    {
        ApplyMovement();
    }
}
