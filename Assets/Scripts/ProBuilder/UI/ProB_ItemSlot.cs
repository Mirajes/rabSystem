using UnityEngine;
using UnityEngine.UI;

public class ProB_ItemSlot : MonoBehaviour
{
    [SerializeField] private ProB_SO_Item _itemData;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _icon;

    public ProB_SO_Item ItemData => _itemData;
    public CanvasGroup CanvasGroup => _canvasGroup;
    public Image Icon => _icon;

    public void UpdateSlot()
    {
        _icon.sprite = _itemData?.Sprite;
    }
}