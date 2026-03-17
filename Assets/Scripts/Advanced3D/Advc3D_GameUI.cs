using TMPro;
using UnityEngine;

public class Advc3D_GameUI : MonoBehaviour 
{
    [SerializeField] private RectTransform _msgPanel;
    [SerializeField] private TMP_Text _msgText;

    public void ShowMsgPanel(string msg)
    {
        _msgText.text = msg;
        _msgPanel.gameObject.SetActive(true);
    }

    public void HideMsgPanel()
    {
        _msgText.text = "";
        _msgPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _msgPanel.gameObject.SetActive(false);
    }
}