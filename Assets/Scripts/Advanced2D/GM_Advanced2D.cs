using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CameraController_2D))]
public class GM_Advanced2D : GameManagerBase
{
    // + CameraController_2D from Core folder
    public Advc2D_PlayerController Player => _player;

    [SerializeField] private Transform _spawnPos;
    private GameObject _playerPrefab;
    private Advc2D_PlayerController _player;

    private CameraController_2D _cameraController;

    public static Action PlayerDeath;

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        PlayerDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        Initializer.Instance.RemoveInputs(this);

        PlayerDeath -= OnPlayerDeath;
    }

    protected override void Init()
    {
        _playerPrefab = Resources.Load<GameObject>("KT_Advc2D/Player");
        GameObject newPlayer = Instantiate(_playerPrefab, _spawnPos.position, _spawnPos.rotation);

        _player = newPlayer.GetComponent<Advc2D_PlayerController>();

        Debug.Log("try to init");
        Initializer.Instance.Advc2D_InitPlayerController(this);


        _cameraController = GetComponent<CameraController_2D>();
        _cameraController.Init(_player.transform);

        Initializer.Instance.EnableInputs();
    }

    private void OnPlayerDeath()
    {
        SceneManager.LoadScene("Advanced2D");
    }
}

