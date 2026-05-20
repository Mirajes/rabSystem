using UnityEngine;
using UnityEngine.EventSystems;

public class ProB_InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] private ProB_ItemSlot _itemSlot;
    [SerializeField] private Transform _parentBeforeDrag;
    [SerializeField] private Transform _mainParent;

    public ProB_ItemSlot ItemSlot => _itemSlot;


    private void Start()
    {
        _mainParent = GM_ProBuilder.Instance.UIManager.GameCanvas.transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parentBeforeDrag = this.transform;

        _itemSlot.transform.SetParent(_mainParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _itemSlot.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _itemSlot.transform.SetParent(_parentBeforeDrag);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ProB_UIManager.ClickItemSlot?.Invoke(_itemSlot.ItemData);
        print("clicked");
    }
}
