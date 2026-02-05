using UnityEngine;

public class GameManagerBase : MonoBehaviour
{
    GameObject _initializerPrefab;

    private void Awake()
    {
        if (FindAnyObjectByType<Initializer>() == null)
        {
            _initializerPrefab = Resources.Load<GameObject>("Core/Initializer");
            Instantiate(_initializerPrefab);
        }
    }
}
