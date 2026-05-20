using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// GameManagerBase nahu
public class GM_ProBuilder : MonoBehaviour
{
    public static GM_ProBuilder Instance => _instance;
    private static GM_ProBuilder _instance;

    public ProB_UIManager UIManager => _uiManager;
    public ProB_SaveManager SaveManager => _saveManager;

    [Header("Core")]
    private ProB_SaveManager _saveManager = new();

    [Header("Links")]
    [SerializeField] private ProB_UIManager _uiManager;
    [SerializeField] private ProB_AudioManager _audioManager;

    [Header("Player")]
    [SerializeField] private ProB_PlayerController _playerPrefab;
    [SerializeField] private Transform _spawn;
    private ProB_PlayerController _player;
    public ProB_PlayerController Player => _player;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(_spawn);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // create Initializer
        if (FindAnyObjectByType<Initializer>() == null)
        {
            Initializer initializerPrefab = Resources.Load<Initializer>("Core/Initializer");
            Instantiate(initializerPrefab);
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _uiManager = FindFirstObjectByType<ProB_UIManager>();
        _audioManager = FindFirstObjectByType<ProB_AudioManager>();

        SaveManager.Load();

        _player = Instantiate(_playerPrefab, _spawn.position, _spawn.rotation);

        Initializer.Instance.ProB_InitPlayerController(this);
        Initializer.Instance.EnableInputs();
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
        Initializer.Instance.RemoveInputs(this);

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("ProBuilder");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        Init();
    }
}

/*
Unity Cinemachine is existng

Coroutine -> UniTask dialogue System
Singleton, DontDestroyOnLoad, PlayerPrefs -> background music playOnAwake in AudioManager, reload on R
Inventory Master -> Inventory + JSON + Drag&Drop
NavMesh -> Navigation Map, Dynamic Map with Obstacles on MousePos by ?Raycast? (3rd eg is complex => x3)
RayCast -> Turret with HighLight

*/