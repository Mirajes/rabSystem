using UnityEngine;

public abstract class A_ItemTool_ProB : MonoBehaviour, IInteracting
{
    [Header("Properties")]
    [SerializeField] protected float _rayDistance = 15f;

    public virtual void MainActivate()
    {
        Debug.Log($"[{this.name} - MainActive]");
    }

    public virtual void SecondaryActivate()
    {
        Debug.Log($"[{this.name} - SecondaryActive]");
    }
}
