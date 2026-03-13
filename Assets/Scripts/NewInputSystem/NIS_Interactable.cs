using UnityEngine;

public abstract class NIS_Interactable : MonoBehaviour
{
    protected GM_NewInputSystem _GM;
    public abstract void Interact();
    private void Awake()
    {
        _GM = FindAnyObjectByType<GM_NewInputSystem>();
    }
}