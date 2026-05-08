using UnityEngine;

public class ProB_AudioManager : MonoBehaviour, ISavable
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _musicVolume = 0.5f;


    private void Start()
    {
        
    }

    public void OnMusicVolumeChange(float volume)
    {
        _musicVolume = volume;
        _audioSource.volume = volume / 100;
        // save
    }

    public void OnLoad()
    {
        
    }

    public void OnSave()
    {
        
    }
}
