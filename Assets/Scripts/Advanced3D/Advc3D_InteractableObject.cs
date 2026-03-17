using UnityEngine;

public abstract class Advc3D_InteractableObject : MonoBehaviour
{
    public string Key => _Key;
    [SerializeField] protected string _Key;

    public virtual void Interact()
    {
        Debug.Log($"i've been interacted - {this.gameObject}");
    }

    public virtual void Restart()
    {
        Debug.Log($"i've been restarted - {this.gameObject}");
    }
}
