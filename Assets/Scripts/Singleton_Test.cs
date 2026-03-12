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
            Instance = this as T;
        else 
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
        Init();
    }

    protected virtual void Init()
    {
        Debug.Log("o");
    }
}
