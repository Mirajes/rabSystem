using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProB_DescriptionWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TMP_Text _itemDescription;

    public void UpdateDescription(ProB_SO_Item item)
    {
        _itemName.text = item.name;
        _itemIcon.sprite = item.Sprite;
        _itemDescription.text = item.Description;

        _itemIcon.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _itemName.text = string.Empty;
        _itemIcon.gameObject.SetActive(false);
        _itemDescription.text = string.Empty;
    }
}