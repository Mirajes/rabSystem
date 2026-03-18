using System.Collections.Generic;
using UnityEngine;

public class Advc3D_GameContext : MonoBehaviour
{
    public static Advc3D_GameContext Instance { get; private set; }

    public int CurrentLevelIndex => _currentLevelIndex;
    public List<Advc3D_Level> Levels => _levels;
    public List<int> CoinCollectedIndex => _coinCollectedIndex;
    public Advc3D_PlayerController Player => _player;
    public Advc3D_Level CurrentLevel => _currentLevelObject;

    [SerializeField] private int _currentLevelIndex = 0;
    [SerializeField] private List<Advc3D_Level> _levels;
    [SerializeField] private List<int> _coinCollectedIndex = new();
    private Advc3D_PlayerController _player;
    private Advc3D_Level _currentLevelObject;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void InitLevel(Advc3D_Level level) 
    {
        _currentLevelObject = level; // monetki ruchkami dobavlyautca
        foreach (var coin in level.Coins)
        {
            if (_coinCollectedIndex.Contains(coin.Index))
                coin.gameObject.SetActive(false);
            else
                coin.gameObject.SetActive(true);
        }
    }

    public void AddCoinToCollected(int index)
    {
        if (_coinCollectedIndex.Contains(index))
            Debug.LogWarning("same indexes");

        _coinCollectedIndex.Add(index);
    }

    public void SwitchLevel(int index)
    {
        _currentLevelIndex = index;
    }
}
