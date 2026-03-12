using UnityEngine;

public abstract class Advc3D_Button : MonoBehaviour
{
    [SerializeField] protected Transform _ButtonTransform;
    [SerializeField] protected Collider _ButtonCollider;

    [SerializeField] protected float _ButtonPressureForce;

    [SerializeField] protected bool _isPressed;

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

    private void OnTriggerEnter(Collider other)
    {
        if (_isPressed) return;

        ButtonPressed();
        _isPressed = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isPressed && !Physics.SphereCast(this.transform.position, 2.5f, Vector3.up, out RaycastHit info, 2.5f, 7))
        {
            ButtonReleased();
            _isPressed = false;
        }
    }
}
