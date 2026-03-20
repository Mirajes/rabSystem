using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Advc3D_Anvil : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _mass;
    [SerializeField] private LayerMask _groundLayer;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.mass = _mass;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GM_Advc3D.Restart();
        }
        else if (((1 << other.gameObject.layer) & _groundLayer.value) != 0)
        {
            Destroy(this.gameObject);
        }
    }
}
