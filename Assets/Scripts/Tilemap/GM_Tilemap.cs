using UnityEngine;

public class GM_Tilemap : GameManagerBase
{
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private CameraController_Tilemap _cameraController;

    private Player_Tilemap _player;

    public Player_Tilemap Player => _player;

    protected override void InitGame()
    {
        _playerPrefab = Resources.Load<GameObject>("KT_Tilemap/Player");

        GameObject newPlayer = Instantiate(_playerPrefab, _spawnPos.position, _spawnPos.rotation);
        _player = newPlayer.GetComponent<Player_Tilemap>();

        if (_cameraController == null) { Debug.LogWarning("where CameraController"); return; }

        _cameraController.Init(_player.transform);
    }

    private void Start()
    {
        InitGame();

        Initializer.Instance.TileMap_InitPlayerController(this);

        Initializer.Instance.EnableInputs();
    }
}
