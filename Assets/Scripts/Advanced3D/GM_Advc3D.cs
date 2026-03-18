using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GM_Advc3D : GameManagerBase
{
    public Advc3D_PlayerController Player => _player;
    public Advc3D_CameraController CameraController => _cameraController;

    [Header("Core")]
    [SerializeField] private Advc3D_GameUI _gameUI;
    [SerializeField] private Advc3D_CameraController _cameraController;

    [Header("Main")]
    private Advc3D_PlayerController _player;
    private Advc3D_Level _level;
    private int _coinBag; // to pref pls

    [Header("Events")]
    public static Action Restart;
    public static Action<Advc3D_Coin> CoinCollect;
    public static Action<int> SwitchLevel;

    protected override void Init()
    {
        var gameContext = Advc3D_GameContext.Instance;
        var spawnPos = gameContext.Levels[gameContext.CurrentLevelIndex].SpawnPos;
        var level = gameContext.Levels[gameContext.CurrentLevelIndex];

        Advc3D_PlayerController playerPrefab = Resources.Load<Advc3D_PlayerController>("KT_Advc3D/Player");
        _level = Instantiate(level, Vector3.zero, Quaternion.identity);
        _player = Instantiate(playerPrefab, spawnPos.position, spawnPos.rotation);
        gameContext.InitLevel(_level);

        if (_gameUI == null) { Debug.LogWarning("—сылки где"); }
        UIService.Instance.Register(_gameUI);
    }

    private void OnSwitchLevel(int lvlIndex)
    {
        Advc3D_GameContext.Instance.SwitchLevel(lvlIndex);
        OnRestart();
    }

    private void OnRestart()
    {
        var gameContext = Advc3D_GameContext.Instance;
        var spawnPos = gameContext.Levels[gameContext.CurrentLevelIndex].SpawnPos;
        var level = gameContext.Levels[gameContext.CurrentLevelIndex];

        Initializer.Instance.RemoveInputs(this);

        // не лучша€ практика -> Advc3D_Level
        Destroy(_level.gameObject);
        Destroy(_player.gameObject);

        var playerPrefab = Resources.Load<Advc3D_PlayerController>("KT_Advc3D/Player");
        _player = Instantiate(playerPrefab, spawnPos.position,spawnPos.rotation);

        _cameraController.SetPlayerTransform(_player.transform);
        
        _level = Instantiate(level, Vector3.zero, Quaternion.identity);
        gameContext.InitLevel(_level); // бред

        _cameraController.OnLevelChange(_level.CameraPoses);

        Initializer.Instance.Advc3D_InitPlayerController(this);
        Initializer.Instance.EnableInputs();
        _player.gameObject.SetActive(true);
        // reset to prev PlayerPref
    }

    public void OnRestartInput(InputAction.CallbackContext context)
    {
        Restart?.Invoke();
    }

    private void OnCoinCollect(Advc3D_Coin coin)
    {
        coin.gameObject.SetActive(false);
        Advc3D_GameContext.Instance.AddCoinToCollected(coin.Index);
        _coinBag += coin.CoinValue;
        coin.OnCoinCollect();
    }

    private void Start()
    {
        Init();

        Initializer.Instance.Advc3D_InitPlayerController(this);
        Initializer.Instance.EnableInputs();
    }

    private void OnEnable()
    {
        Restart += OnRestart;
        CoinCollect += OnCoinCollect;
        SwitchLevel += OnSwitchLevel;
    }

    private void OnDisable()
    {
        Restart -= OnRestart;
        CoinCollect -= OnCoinCollect;
        SwitchLevel -= OnSwitchLevel;
        Initializer.Instance.RemoveInputs(this);
    }

    private void OnDestroy()
    {
        UIService.Instance.Clear();
    }
}
