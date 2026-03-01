using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CameraController_2D))]
public class GM_Advanced2D : GameManagerBase
{
    // + CameraController_2D from Core folder
    public Advc2D_Player Player => _player;

    [SerializeField] private Transform _spawnPos;
    private GameObject _playerPrefab;
    private Advc2D_Player _player;

    private CameraController_2D _cameraController;

    public static Action PlayerHit;

    private void Start()
    {
        Init();
    }
    
    private void OnEnable()
    {
        PlayerHit += OnPlayerHit;
    }

    private void OnDisable()
    {
        Initializer.Instance.RemoveInputs(this);

        PlayerHit -= OnPlayerHit;
    }

    protected override void Init()
    {
        _playerPrefab = Resources.Load<GameObject>("KT_Advc2D/Player");
        GameObject newPlayer = Instantiate(_playerPrefab, _spawnPos.position, _spawnPos.rotation);

        _player = newPlayer.GetComponent<Advc2D_Player>();

        Initializer.Instance.Advc2D_InitPlayerController(this);


        _cameraController = GetComponent<CameraController_2D>();
        _cameraController.Init(_player.transform);

        Initializer.Instance.EnableInputs();
    }

    private void OnPlayerHit()
    {
        if (_player.IsShielded)
        {
            _player.DestroyShield();
        }
        else
        {
            SceneManager.LoadScene("Advanced2D");
        }

    }
}

