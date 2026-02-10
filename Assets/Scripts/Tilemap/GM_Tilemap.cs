using UnityEngine;

public class GM_Tilemap : GameManagerBase
{
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private CameraController_2D _cameraController;

    private Player_Tilemap _player;

    public Player_Tilemap Player => _player;

    protected override void Init()
    {
        _playerPrefab = Resources.Load<GameObject>("KT_Tilemap/Player");

        GameObject newPlayer = Instantiate(_playerPrefab, _spawnPos.position, _spawnPos.rotation);
        _player = newPlayer.GetComponent<Player_Tilemap>();

        _cameraController = FindAnyObjectByType<CameraController_2D>();
        if (_cameraController == null) { Debug.LogWarning("where CameraController_2D"); return; }

        _cameraController.Init(_player.transform);
    }

    private void Start()
    {
        Init();

        Initializer.Instance.TileMap_InitPlayerController(this);

        Initializer.Instance.EnableInputs();
    }
}
