using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Advc3D_LvlGrabbingBox : Advc3D_InteractableObject, IGrabbable
{
    private Rigidbody _rb;
    private Collider _collider;

    private void OnEnable()
    {
        _rb = this.transform.GetComponent<Rigidbody>();
        _collider = this.transform.GetComponent<Collider>();
    }

    public void OnGrab()
    {
        _rb.isKinematic = true;
    }

    public void OnRelease()
    {
        _rb.isKinematic = false;
    }
}