using UnityEngine;

public class Advc3D_LvlButton : Advc3D_Button
{
    [Header("LVL")]
    [SerializeField] private Advc3D_InteractableObject _interactableObj;

    protected override void ButtonPressed()
    {
        base.ButtonPressed();

        _interactableObj.Interact();
    }

    protected override void ButtonReleased()
    {
        base.ButtonReleased();

        _interactableObj.Restart();
    }
}
