using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GM_Doom : GameManagerBase
{
    public DOT_PlayerController Player => _player;
    public bool IsPlaying => _isPlaying;

    [Header("Main")]
    [SerializeField] private Transform _spawnPos;
    private DOT_PlayerController _player;
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

    [Header("DisplayInput")]
    [SerializeField] private Image _inputProgressPanel;

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

    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    private void InitTimer()
    {
        if (_progressBar == null) { Debug.LogWarning("no bar"); return; }
        if (_timerText == null) { Debug.LogWarning("no text"); return; }

        _isPlaying = true;

        _remainingTime = _timeToDie;
        _timerText.text = _remainingTime.ToString();

        _progressBar.DOValue(1f, _timeToDie);
    }

    private void InitMinigame()
    {
        if (_ceiling == null || _ceilingStartTransform == null || _ceilingEndTransform == null) { Debug.LogWarning("no ref to ceiling"); return; }

        _ceiling.transform.position = _ceilingStartTransform.position;
        _ceiling.transform.DOMove(_ceilingEndTransform.position, _timeToDie);

        for (int i = 0; i < _inputProgressPanel.transform.childCount; i++) // to turn all images to black
        {
            TurnInputImageToBlack(i);

        }
    }

    public void TurnInputImageToBlack(int index)
    {
        _inputProgressPanel.transform.GetChild(index).GetChild(0).GetComponent<Image>().fillAmount = 0;
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

    public void TogglePauseMinigame()
    {
        _isPlaying = !_isPlaying;
        DOTween.TogglePauseAll();
    }

    private void CountTimer()
    {
        if (!_isPlaying) return;

        _remainingTime -= Time.deltaTime;

        int minutes = (int)_remainingTime / 60;
        int secs = (int)_remainingTime % 60;

        _timerText.text = $"{minutes}:{secs}";
    }

    public void OnInputAction(int index, float fillTime)
    {
        _inputProgressPanel.transform.GetChild(index).GetChild(0).GetComponent<Image>().DOFillAmount(1, fillTime);
    }

    public void OnInputCancelled(int index) => TurnInputImageToBlack(index);
}


public enum Actions
{
    Move,
    Rotate,
    Jump
}