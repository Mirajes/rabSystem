using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// GameManagerBase nahu
public class GM_ProBuilder : MonoBehaviour
{
    public static GM_ProBuilder Instance => _instance;
    private static GM_ProBuilder _instance;

    [SerializeField] private PB_PlayerController _player;


    public PB_PlayerController Player => _player;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            
            // create Initializer
            if (FindAnyObjectByType<Initializer>() == null)
            {
                Initializer initializerPrefab = Resources.Load<Initializer>("Core/Initializer");
                Instantiate(initializerPrefab);
            }
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        Initializer.Instance.RemoveInputs(this); // question about it
        // how to RemoveInput only when scene is different
    }

    private void Init()
    {
        Initializer.Instance.ProB_InitPlayerController(this);
        Initializer.Instance.EnableInputs();
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("ProBuilder");
    }
}

/*
Unity Cinemachine is existng

Coroutine -> UniTask dialogue System
Singleton, DontDestroyOnLoad, PlayerPrefs -> background music playOnAwake in AudioManager, reload on R
*/