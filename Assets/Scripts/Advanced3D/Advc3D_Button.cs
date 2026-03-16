using UnityEngine;

public abstract class Advc3D_Button : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] protected Transform _ButtonTransform;
    [SerializeField] protected Collider _ButtonCollider;
    [SerializeField] protected float _ButtonPressureForce = 0.3f;
    [SerializeField] protected bool _IsPressed = false;
    [SerializeField] protected LayerMask _EntityLayerMask = 7;

    protected virtual void ButtonPressed()
    {
        _ButtonTransform.localScale = new Vector3(
            _ButtonTransform.localScale.x,
            _ButtonPressureForce,
            _ButtonTransform.localScale.z);
    }
    protected virtual void ButtonReleased()
    {
        _ButtonTransform.localScale = Vector3.one;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (_IsPressed) return;

        ButtonPressed();
        _IsPressed = true;
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (_IsPressed && !IsEntityInside())
        {
            ButtonReleased();
            _IsPressed = false;
        }
    }

    private bool IsEntityInside()
    {
        Collider[] hit = Physics.OverlapBox(transform.position, _ButtonCollider.transform.localScale * 0.5f, Quaternion.identity, _EntityLayerMask);
        print($"colliders inside -- {hit.Length}");
        return hit.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _ButtonCollider.transform.localScale);
    }
}
