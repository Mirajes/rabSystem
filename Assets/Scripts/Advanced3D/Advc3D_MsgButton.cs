using UnityEngine;

public class Advc3D_MsgButton : Advc3D_Button
{
    [Header("Message")]
    [SerializeField] private string _msg;

    protected override void ButtonPressed()
    {
        base.ButtonPressed();

        var gameUI = UIService.Instance.Get<Advc3D_GameUI>();
        gameUI.ShowMsgPanel(_msg);
    }

    protected override void ButtonReleased()
    {
        base.ButtonReleased();

        var gameUI = UIService.Instance.Get<Advc3D_GameUI>();
        gameUI.HideMsgPanel();
    }
}