using UnityEngine;

public abstract class Singleton_Test<T> : MonoBehaviour where T : class 
{
    public static T Instance
    {
        get; private set; 
    }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        Init();
    }

    protected virtual void Init()
    {
        Debug.Log("o");
    }
}
