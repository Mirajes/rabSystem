using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GM_Advc3D : GameManagerBase
{
    public Advc3D_PlayerController Player => _player;
    public Advc3D_CameraController CameraController => _cameraController;
    public Advc3D_GameUI GameUI => _gameUI;

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
    public static Action<Material, int> BuySkin;

    protected override void Init()
    {
        if (_gameUI == null) { Debug.LogWarning("—сылки где"); }
        UIService.Instance.Register(_gameUI);

        var gameContext = Advc3D_GameContext.Instance;
        var spawnPos = gameContext.Levels[gameContext.CurrentLevelIndex].SpawnPos;
        var level = gameContext.Levels[gameContext.CurrentLevelIndex];

        Advc3D_PlayerController playerPrefab = Resources.Load<Advc3D_PlayerController>("KT_Advc3D/Player");
        _level = Instantiate(level, Vector3.zero, Quaternion.identity);
        _player = Instantiate(playerPrefab, spawnPos.position, spawnPos.rotation);

        _cameraController.SetPlayerTransform(_player.transform);
        _cameraController.OnLevelChange(_level.CameraPoses);

        gameContext.InitLevel(_level);

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
        var gameUI = UIService.Instance.Get<Advc3D_GameUI>();

        gameUI.OnRestart();
        DOTween.KillAll();
        Initializer.Instance.RemoveInputs(this);


        _cameraController.MainCamera.transform.parent = null;

        // не лучша€ практика -> Advc3D_Level
        Destroy(_level.gameObject);
        Destroy(_player.gameObject);

        _level = Instantiate(level, Vector3.zero, Quaternion.identity);
        gameContext.InitLevel(_level); // бред

        var playerPrefab = Resources.Load<Advc3D_PlayerController>("KT_Advc3D/Player");
        _player = Instantiate(playerPrefab, spawnPos.position, spawnPos.rotation);
        
        _cameraController.SetPlayerTransform(_player.transform);
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

    public void OnCoinCollect(Advc3D_Coin coin)
    {
        coin.gameObject.SetActive(false);
        Advc3D_GameContext.Instance.AddCoinToCollected(coin.Index);
        _coinBag += coin.CoinValue;
        coin.OnCoinCollect();

        var gameUI = UIService.Instance.Get<Advc3D_GameUI>();
        gameUI.UpdateCoinBagValue(_coinBag);
    }

    public void OnBuySkin(Material skin, int cost)
    {
        var gameUI = UIService.Instance.Get<Advc3D_GameUI>();

        if (_coinBag < cost)
        {
            gameUI.ShowMsgPanel("u haven't enough money");
            return;
        }
        else if (_coinBag >= cost)
        {
            _player.Renderer.material = skin;
            gameUI.ShowMsgPanel("NOW U C-O-O-L");
        }
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
        BuySkin += OnBuySkin;
    }

    private void OnDisable()
    {
        Restart -= OnRestart;
        CoinCollect -= OnCoinCollect;
        SwitchLevel -= OnSwitchLevel;
        BuySkin -= OnBuySkin;

        Initializer.Instance.RemoveInputs(this);
    }

    private void OnDestroy()
    {
        UIService.Instance.Clear();
    }
}
