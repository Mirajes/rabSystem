using UnityEngine;
using UnityEngine.UI;

public class ProB_ItemSlot : MonoBehaviour
{
    [SerializeField] private ProB_SO_Item _itemData;
    [SerializeField] private Image _icon;

    public ProB_SO_Item ItemData => _itemData;
}