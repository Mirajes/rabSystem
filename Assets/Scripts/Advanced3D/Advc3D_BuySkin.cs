using UnityEngine;

public class Advc3D_BuySkin : MonoBehaviour
{
    [SerializeField] private Material _skinMaterial;
    [SerializeField] private int _cost;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GM_Advc3D.BuySkin?.Invoke(_skinMaterial, _cost);
    }
}
