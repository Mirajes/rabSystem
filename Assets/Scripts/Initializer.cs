using UnityEngine;

public class Initializer : MonoBehaviour
{
    private GameManager _gameManager;

    private InputSystem_Actions _inputs;

    [SerializeField] private GameObject _player;

    private void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _gameManager.Init(_player);
    }

    private void OnEnable()
    {
        _inputs = new();
        _inputs.Player.Move.performed += context => _gameManager.PlayerController.OnMove(context.ReadValue<Vector2>());
        _inputs.Player.Move.canceled += context => _gameManager.PlayerController.OnMove(Vector2.zero); 

        _inputs.Player.Jump.started += _ => _gameManager.PlayerController.OnJump();
    }

    private void Start()
    {
        _inputs.Player.Enable();
        
    }

    private void OnDisable()
    {
        _inputs.Disable();
        _inputs.Dispose();
    }
}
