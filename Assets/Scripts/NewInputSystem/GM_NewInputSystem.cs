using System;
using UnityEngine;

public class GM_NewInputSystem : GameManagerBase
{
    [SerializeField] private Transform _spawnPos;
    private PlayerController _playerController;

    private Transport _currentTransport;
    public Action CarSummon;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _carPrefab;

    public PlayerController PlayerController => _playerController;
    public Transport CurrentTransport => _currentTransport;
    public GameObject CarPrefab => _carPrefab;

    protected override void Init()
    {
        GameObject newPlayer = Instantiate(_player, _spawnPos.position, _spawnPos.rotation);
        _playerController = newPlayer.GetComponent<PlayerController>();

        CarSummon += OnCarSummon;

        _currentTransport = FindAnyObjectByType<Transport>(); // ya ne znaui kak poluchit' dostup k kodu bez obj
    }

    public void OnCarSummon()
    {
        Transport prevCar = FindAnyObjectByType<Transport>();
        if (prevCar != null) Destroy(prevCar.gameObject);

        GameObject carObj = Instantiate(_carPrefab);
        Transport newCar = carObj.GetComponent<Transport>();
        _currentTransport = newCar;

        newCar.transform.position = _playerController.transform.position + _playerController.transform.forward * 5f;
        newCar.transform.rotation = Quaternion.LookRotation(_playerController.transform.forward);

        Initializer.Instance.NIS_InitCarControll(this);
    }

    public void ChangeMap(bool isInCar)
    {
        _playerController._isInsideCar = isInCar;

        if (_playerController._isInsideCar)
        {
            Initializer._inputs.Player_NIS.Disable();
            Initializer._inputs.Transport.Enable();
        }
        else
        {
            Initializer._inputs.Player_NIS.Enable();
            Initializer._inputs.Transport.Disable();
        }
    }

    private void Start()
    {
        Init();

        Initializer.Instance.NIS_InitDefaultPlayerControll(this);
        Initializer.Instance.NIS_InitCarControll(this);

        Initializer.Instance.EnableInputs();
    }
}