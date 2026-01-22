using UnityEngine;

public class Initializer : MonoBehaviour
{
    [Header("Mono")]
    private GameManager _gameManager;

    [Header("Main")]
    public static InputSystem_Actions _inputs;

    [Header("Prefabs")]
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _carPrefab;

    public GameObject CarPrefab => _carPrefab;

    private void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _gameManager.Init(_player);
    }

    private void OnEnable()
    {
        _inputs = new();

        InitDefaultPlayerControll();

        InitCarControll();
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

    public void InitCarControll()
    {
        // Transport

        _inputs.Transport.Zoom.performed += context => _gameManager.PlayerController.OnZoomInput(context.ReadValue<Vector2>());

        _inputs.Transport.Look.performed += context => _gameManager.PlayerController.OnRotateInput(context.ReadValue<Vector2>());
        _inputs.Transport.Look.canceled += _ => _gameManager.PlayerController.OnRotateInput(Vector2.zero);

        _inputs.Transport.Move.performed += context => _gameManager.CurrentTransport.OnControllerInput(context.ReadValue<Vector2>());
        _inputs.Transport.Move.canceled += _ => _gameManager.CurrentTransport.OnControllerInput(Vector2.zero);

        _inputs.Transport.CarExit.started += _ => _gameManager.ChangeMap(false);
    }
    public void InitDefaultPlayerControll()
    {
        // Player
        _inputs.Player.Move.performed += context => _gameManager.PlayerController.OnMoveInput(context.ReadValue<Vector2>());
        _inputs.Player.Move.canceled += context => _gameManager.PlayerController.OnMoveInput(Vector2.zero);

        _inputs.Player.Jump.started += _ => _gameManager.PlayerController.OnJumpInput();

        _inputs.Player.Look.performed += context => _gameManager.PlayerController.OnRotateInput(context.ReadValue<Vector2>());
        _inputs.Player.Look.canceled += context => _gameManager.PlayerController.OnRotateInput(Vector2.zero);

        _inputs.Player.Zoom.performed += context => _gameManager.PlayerController.OnZoomInput(context.ReadValue<Vector2>());

        _inputs.Player.CarSummon.started += _ => _gameManager.PlayerController.OnCarSummonInput();
    }
    public void InitDoomPlayerControll()
    {
        //_inputs.CameraTest.Look.performed += callback => 
    }
}