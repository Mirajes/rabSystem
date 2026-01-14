using UnityEngine;

public class GameManager: MonoBehaviour
{
    private Controller _playerController;

    public Controller PlayerController => _playerController;

    public void Init(GameObject player)
    {
        GameObject newPlayer = Instantiate(player);
        _playerController = newPlayer.GetComponent<Controller>();
    }
}