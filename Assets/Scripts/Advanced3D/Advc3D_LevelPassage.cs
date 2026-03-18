using UnityEngine;

public class Advc3D_LevelPassage : MonoBehaviour
{
    [SerializeField] private int _levelIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GM_Advc3D.SwitchLevel(_levelIndex);
        }
    }
}