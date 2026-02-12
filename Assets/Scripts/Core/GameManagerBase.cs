using UnityEngine;

public abstract class GameManagerBase : MonoBehaviour
{
    GameObject _initializerPrefab;

    protected abstract void Init();

    private void Awake()
    {
        if (FindAnyObjectByType<Initializer>() == null)
        {
            _initializerPrefab = Resources.Load<GameObject>("Core/Initializer");
            Instantiate(_initializerPrefab);
        }   
    }

    private void OnEnable()
    {
        Initializer.Instance.EnableInputs();
    }

    private void OnDestroy()
    {

    }
}
