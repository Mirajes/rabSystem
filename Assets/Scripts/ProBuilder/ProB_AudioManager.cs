using UnityEngine;

public class ProB_AudioManager : MonoBehaviour, ISavable
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _musicVolume = 0.5f;

    public float MusicVolume => _musicVolume;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        ProB_SaveManager.OnSave += OnSave;
        ProB_SaveManager.OnLoad += OnLoad;
    }

    private void OnDisable()
    {
        ProB_SaveManager.OnSave -= OnSave;
        ProB_SaveManager.OnLoad -= OnLoad;
    }

    public void OnMusicVolumeChange(float volume)
    {
        _musicVolume = volume;
        _audioSource.volume = volume / 100;
    }

    public void OnSave(SaveData data)
    {
        data.Data.Add("MusicVolume", _musicVolume.ToString("F2"));
    }

    public void OnLoad(SaveData data)
    {
        if (!data.Data.ContainsKey("MusicVolume"))
            return;

        _musicVolume = float.Parse(data.Data["MusicVolume"]);
        Debug.Log($"[AudioManager] - MusicVolume is {_musicVolume}");
    }
}
