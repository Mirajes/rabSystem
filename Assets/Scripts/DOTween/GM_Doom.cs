using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GM_Doom : GameManagerBase
{
    public DOT_PlayerController Player => _player;
    private DOT_PlayerController _player;

    [Header("Main")]
    [SerializeField] private Transform _spawnPos;
    private GameObject _playerPrefab;

    [Header("Camera")]
    private Camera _camera;
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0, 0.75f, 0f);

    [Header("Timer")]
    [SerializeField] private float _timeToDie = 120f;
    private float _remainingTime;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Slider _progressBar;
    private bool _isPlaying = false;

    [Header("Minigame")]
    [SerializeField] private GameObject _ceiling;
    [SerializeField] private Transform _ceilingStartTransform;
    [SerializeField] private Transform _ceilingEndTransform;

    private Tween _tweenFallingCeiling;
    private Tween _tweenBar;

    private void Start()
    { 
        Init();
        InitTimer();
        InitMinigame();

        Initializer.Instance.DOTween_InitDoomPlayerControll(this);
        Initializer.Instance.EnableInputs();
    }

    private void Update()
    {
        CountTimer();
    }

    private void InitTimer()
    {
        if (_progressBar == null) { Debug.LogWarning("no bar"); return; }
        if (_timerText == null) { Debug.LogWarning("no text"); return; }

        _isPlaying = true;

        _remainingTime = _timeToDie;
        _timerText.text = _remainingTime.ToString();

        _tweenBar = _progressBar.DOValue(1f, _timeToDie);
    }
    
    private void InitMinigame()
    {
        if (_ceiling == null || _ceilingStartTransform == null || _ceilingEndTransform == null) { Debug.LogWarning("no ref to ceiling"); return; }

        _ceiling.transform.position = _ceilingStartTransform.position;
        _tweenFallingCeiling = _ceiling.transform.DOMove(_ceilingEndTransform.position, _timeToDie);
    }

    public void TogglePauseMinigame()
    {
        _isPlaying = !_isPlaying;
        _tweenFallingCeiling.TogglePause();
        _tweenBar.TogglePause();

        if (_isPlaying)
            Initializer.Instance.EnableInputs();
        else 
            Initializer.Instance.DisableInputs();
    }

    private void CountTimer()
    {
        if (!_isPlaying) return;

        _remainingTime -= Time.deltaTime;

        int minutes = (int)_remainingTime / 60;
        int secs = (int)_remainingTime % 60;

        _timerText.text = $"{minutes}:{secs}";
    }

    protected override void Init()
    {
        _playerPrefab = Resources.Load<GameObject>("KT_DOTween/DoomPlayer");

        GameObject newPlayer = Instantiate(_playerPrefab, _spawnPos.position, _spawnPos.rotation);
        _player = newPlayer.GetComponent<DOT_PlayerController>();

        _camera = Camera.main;
        _camera.transform.position = _player.transform.position + _cameraOffset;
        _camera.transform.rotation = _player.transform.rotation;
        _camera.transform.parent = _player.transform;
    }


    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}
