using System;
using UnityEngine;

public class GM_Advc3D : GameManagerBase
{
    public Advc3D_PlayerController Player => _player;

    [Header("Core")]
    [SerializeField] private Advc3D_GameUI _gameUI;

    [Header("Main")]
    private Advc3D_PlayerController _player;
    private Advc3D_Level _level;

    [Header("Events")]
    public static Action Restart;

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

    private void OnRestart()
    {
        var gameContext = Advc3D_GameContext.Instance;
        var spawnPos = gameContext.Levels[gameContext.CurrentLevelIndex].SpawnPos;
        var level = gameContext.Levels[gameContext.CurrentLevelIndex];

        // не лучша€ практика -> Advc3D_Level
        Destroy(_level.gameObject);

        _player.gameObject.SetActive(false);
        _player.gameObject.transform.position = spawnPos.position;
        _player.gameObject.SetActive(true);

        _level = Instantiate(level, Vector3.zero, Quaternion.identity);
        gameContext.InitLevel(_level); // бред

        // reset to prev PlayerPref
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
    }

    private void OnDisable()
    {
        Restart -= OnRestart;
        Initializer.Instance.RemoveInputs(this);
    }

    private void OnDestroy()
    {
        UIService.Instance.Clear();
    }
}
