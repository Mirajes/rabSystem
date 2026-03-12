using UnityEngine;

public class Advc3D_MsgButton : Advc3D_Button
{
    [SerializeField] private string _msg;

    protected override void ButtonPressed()
    {
        var gameUI = UIService.Instance.Get<Advc3D_GameUI>();
        gameUI.ShowMsgPanel(_msg);
    }

    protected override void ButtonReleased()
    {
        var gameUI = UIService.Instance.Get<Advc3D_GameUI>();
        gameUI.HideMsgPanel();
    }
}
