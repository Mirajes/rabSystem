using UnityEngine;

public class Advc3D_LvlButton : Advc3D_Button
{
    [Header("LVL")]
    [SerializeField] private Advc3D_InteractableObject _interactableObject;

    protected override void ButtonPressed()
    {
        base.ButtonPressed();

        _interactableObject.Interact();
    }

    protected override void ButtonReleased()
    {
        base.ButtonReleased();

        _interactableObject.Restart();
    }
}
