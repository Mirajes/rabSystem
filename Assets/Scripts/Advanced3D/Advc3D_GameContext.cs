using System.Collections.Generic;
using UnityEngine;

public class Advc3D_GameContext : MonoBehaviour
{
    public static Advc3D_GameContext Instance { get; private set; }

    public int CurrentLevelIndex => _currentLvl;
    public List<Advc3D_Level> Levels => _levels;
    public Advc3D_PlayerController Player => _player;
    public Advc3D_Level CurrentLevel => _currentLevel;

    [SerializeField] private int _currentLvl = 0;
    [SerializeField] private List<Advc3D_Level> _levels;
    private Advc3D_PlayerController _player;
    private Advc3D_Level _currentLevel;

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

    public void InitLevel(Advc3D_Level level) => _currentLevel = level;
}
