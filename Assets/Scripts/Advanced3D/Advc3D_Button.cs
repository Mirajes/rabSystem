using UnityEngine;

public abstract class Advc3D_Button : MonoBehaviour
{
    [SerializeField] protected Collider _ButtonCollider;

    protected abstract void ButtonPressed();
    protected abstract void ButtonReleased();

    private void OnTriggerEnter(Collider other)
    {
        ButtonPressed();
    }

    private void OnTriggerExit(Collider other)
    {
        ButtonReleased();
    }
}
