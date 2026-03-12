using UnityEngine;

public class GM_Advc3D : GameManagerBase
{
    public Advc3D_PlayerController Player => _player;

    [Header("Core")]
    [SerializeField] private Advc3D_GameUI _gameUI;

    [Header("Main")]
    [SerializeField] private Transform _spawnPos;
    private Advc3D_PlayerController _player;

    protected override void Init()
    {
        Advc3D_PlayerController playerPrefab = Resources.Load<Advc3D_PlayerController>("KT_Advc3D/Player");
        _player = Instantiate(playerPrefab, _spawnPos.position, _spawnPos.rotation);

        if (_gameUI == null) { Debug.LogWarning("—сылки где"); }
        UIService.Instance.Register(_gameUI);
    }

    private void Start()
    {
        Init();

        Initializer.Instance.Advc3D_InitPlayerController(this);
        Initializer.Instance.EnableInputs();
    }

    private void OnDisable()
    {
        Initializer.Instance.RemoveInputs(this);
    }

    private void OnDestroy()
    {
        UIService.Instance.Clear();
    }
}
