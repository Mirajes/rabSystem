using UnityEngine;

public class GM_Advc3D : GameManagerBase
{
    [SerializeField] private Transform _spawnPos;
    private Advc3D_PlayerController _playerPrefab;

    private Advc3D_PlayerController _player;

    public Advc3D_PlayerController Player => _player;

    protected override void Init()
    {
        _playerPrefab = Resources.Load<Advc3D_PlayerController>("KT_Advc3D/Player");
        _player = Instantiate(_playerPrefab, _spawnPos.position, _spawnPos.rotation);

        Initializer.Instance.Advc3D_InitPlayerController(this);
    }

    private void Start()
    {

        Init();
        Initializer.Instance.EnableInputs();
    }

    private void OnDisable()
    {
        Initializer.Instance.RemoveInputs(this);
    }
}
