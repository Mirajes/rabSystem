using UnityEngine;

public class GameContext<T> : MonoBehaviour where T : class
{
    private static T _instance;
    public static T Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
