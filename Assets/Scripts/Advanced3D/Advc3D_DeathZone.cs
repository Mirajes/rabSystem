using UnityEngine;

public class Advc3D_DeathZone : MonoBehaviour
{
    [SerializeField] private Collider _deathZone;

    private void OnTriggerEnter(Collider other)
    {
        if (_deathZone == null) return;

        if (other.CompareTag("Player"))
            GM_Advc3D.Restart?.Invoke();
    }
}