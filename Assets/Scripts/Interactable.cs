using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected GameManager _GM;

    public abstract void Interact();

    private void Awake()
    {
        _GM = FindAnyObjectByType<GameManager>();
    }
}
