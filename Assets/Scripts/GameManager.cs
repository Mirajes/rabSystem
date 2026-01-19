using System;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    [SerializeField] private Transform _spawnPos;
    private PlayerController _playerController;
    private Initializer _initializer;

    [SerializeField] private Transport _transport;
    public Action CarSummon;

    public PlayerController PlayerController => _playerController;
    public Transport Transport => _transport;

    public void Init(GameObject player)
    {
        _initializer = FindAnyObjectByType<Initializer>();

        GameObject newPlayer = Instantiate(player, _spawnPos.position, _spawnPos.rotation);
        _playerController = newPlayer.GetComponent<PlayerController>();

        CarSummon += OnCarSummon;

        _transport = FindAnyObjectByType<Transport>(); // ya ne znaui kak poluchit' dostup k kodu bez obj
    }
    
    public void OnCarSummon()
    {
        Transport prevCar = FindAnyObjectByType<Transport>();
        if (prevCar != null) Destroy(prevCar.gameObject);

        Transport newCar = Instantiate(_transport);
        _transport = newCar;
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