using System;
using UnityEngine;

public class GM_NewInputSystem : GameManagerBase
{
    [SerializeField] private Transform _spawnPos;
    private PlayerController _playerController;
    private Initializer _initializer;

    private Transport _currentTransport;
    public Action CarSummon;

    public PlayerController PlayerController => _playerController;
    public Transport CurrentTransport => _currentTransport;

    public void Init(GameObject player)
    {
        _initializer = FindAnyObjectByType<Initializer>();

        GameObject newPlayer = Instantiate(player, _spawnPos.position, _spawnPos.rotation);
        _playerController = newPlayer.GetComponent<PlayerController>();

        CarSummon += OnCarSummon;

        _currentTransport = FindAnyObjectByType<Transport>(); // ya ne znaui kak poluchit' dostup k kodu bez obj
    }

    public void OnCarSummon()
    {
        Transport prevCar = FindAnyObjectByType<Transport>();
        if (prevCar != null) Destroy(prevCar.gameObject);

        GameObject carObj = Instantiate(_initializer.CarPrefab);
        Transport newCar = carObj.GetComponent<Transport>();
        _currentTransport = newCar;

        newCar.transform.position = _playerController.transform.position + _playerController.transform.forward * 5f;
        newCar.transform.rotation = Quaternion.LookRotation(_playerController.transform.forward);

        _initializer.InitCarControll();
    }

    public void ChangeMap(bool isInCar)
    {
        _playerController._isInsideCar = isInCar;

        if (_playerController._isInsideCar)
        {
            Initializer._inputs.Player.Disable();
            Initializer._inputs.Transport.Enable();
        }
        else
        {
            Initializer._inputs.Player.Enable();
            Initializer._inputs.Transport.Disable();
        }
    }
}