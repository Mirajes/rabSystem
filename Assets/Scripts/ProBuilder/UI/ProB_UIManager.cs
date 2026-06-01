using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ProB_UIManager : MonoBehaviour
{
    [Header("link")]
    [SerializeField] private ProB_AudioManager _audioManager;

    [Header("Game")]
    [SerializeField] private Canvas _gameCanvas;

    [Header("Settings")]
    [SerializeField] private Canvas _settingsCanvas;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private TMP_Text _musicValueText;

    [Header("Inventory")]
    [SerializeField] private ProB_DescriptionWindow _descriptionWindow;
    [SerializeField] private RectTransform _invTransform;
    [SerializeField] private Button _invCloseButton;

    public Canvas GameCanvas => _gameCanvas;

    public static Action<ProB_SO_Item> ClickItemSlot;

    private void Start()
    {
        _musicSlider.onValueChanged.AddListener(_audioManager.OnMusicVolumeChange);
        _musicSlider.onValueChanged.AddListener(OnMusicVolumeChange);
    }

    private void OnEnable()
    {
        ClickItemSlot += OnClickItemSlot;
        _invCloseButton.onClick.AddListener(OnInvCloseButtonClicked);
    }

    private void OnDisable()
    {
        ClickItemSlot -= OnClickItemSlot;
        _invCloseButton.onClick?.RemoveListener(OnInvCloseButtonClicked);
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
            GM_ProBuilder.Instance.SaveManager.Save();
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log($"[{this.name}] - Cursor Locked");

            Initializer.Inputs.ProB_Player.Enable();
            _settingsCanvas.gameObject.SetActive(false);
        }
        else
        {
            UpdateUI();
            Cursor.lockState = CursorLockMode.None;
            Debug.Log($"[{this.name}] - Cursor Unlocked");

            Initializer.Inputs.ProB_Player.Disable();
            _settingsCanvas.gameObject.SetActive(true);
        }
    }

    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        if (_invTransform.gameObject.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            // save inventory pls
            
            Initializer.Inputs.ProB_Player.Enable();
            _invTransform.gameObject.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;

            Initializer.Inputs.ProB_Player.Disable();
            _invTransform.gameObject.SetActive(true);

        }
    }

    private void UpdateUI()
    {
        _musicSlider.value = _audioManager.MusicVolume;
        OnMusicVolumeChange(_musicSlider.value);
    }

    private void OnClickItemSlot(ProB_SO_Item item)
    {
        _descriptionWindow.UpdateDescription(item);
    }

    private void OnInvCloseButtonClicked()
    {
        Cursor.lockState= CursorLockMode.Locked;
        Initializer.Inputs.ProB_Player.Enable();
        _invTransform.gameObject.SetActive(false);
    }
}
