using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ProB_UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Canvas _settingsCanvas;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private TMP_Text _musicValueText;

    [Header("smth")]
    [SerializeField] private ProB_AudioManager _audioManager;


    private void Start()
    {
        _musicSlider.onValueChanged.AddListener(_audioManager.OnMusicVolumeChange);
        _musicSlider.onValueChanged.AddListener(OnMusicVolumeChange);
    }

    private void OnDestroy()
    {
        _musicSlider.onValueChanged.RemoveAllListeners();
    }

    private void OnMusicVolumeChange(float value)
    {
        _musicValueText.text = value.ToString("F2");
    }

    public void OnSettingsInput(InputAction.CallbackContext context)
    {
        if (_settingsCanvas.gameObject.activeSelf)
        {
            _settingsCanvas.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log($"[{this.name}] - Cursor Locked");
        }
        else
        {
            _settingsCanvas.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Debug.Log($"[{this.name}] - Cursor Unlocked");
        }
    }
}
