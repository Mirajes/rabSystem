using UnityEngine;

public abstract class GameManagerBase : MonoBehaviour
{
    GameObject _initializerPrefab;
    
    protected abstract void InitGame();

    private void Awake()
    {
        if (FindAnyObjectByType<Initializer>() == null)
        {
            _initializerPrefab = Resources.Load<GameObject>("Core/Initializer");
            Instantiate(_initializerPrefab);
        }
    }
}
