using UnityEngine;

public abstract class A_ItemTool_ProB : MonoBehaviour, IInteracting
{
    [SerializeField] protected GameObject _Prefab;

    public virtual void Activate()
    {
        Debug.Log($"[{this.name} - Is Activated]");
    }
}
