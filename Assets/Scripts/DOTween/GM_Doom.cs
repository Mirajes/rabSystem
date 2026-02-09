using UnityEngine;

public class GM_Doom : GameManagerBase
{
    [SerializeField] private Transform _spawnPos;

    private GameObject _playerPrefab;
    private DOT_PlayerController _player;

    private Camera _camera;
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0, 0.75f, 0f);

    public DOT_PlayerController Player => _player;

    protected override void InitGame()
    {
        _playerPrefab = Resources.Load<GameObject>("KT_DOTween/DoomPlayer");

        GameObject newPlayer = Instantiate(_playerPrefab, _spawnPos.position, _spawnPos.rotation);
        _player = newPlayer.GetComponent<DOT_PlayerController>();

        _camera = Camera.main;
        _camera.transform.position = _player.transform.position + _cameraOffset;
        _camera.transform.rotation = _player.transform.rotation;
        _camera.transform.parent = _player.transform;
    }

    private void Start()
    {
        InitGame();

        Initializer.Instance.DOTween_InitDoomPlayerControll(this);
        Initializer.Instance.EnableInputs();
    }
}
