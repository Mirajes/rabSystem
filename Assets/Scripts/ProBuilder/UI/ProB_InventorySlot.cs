using UnityEngine;
using UnityEngine.EventSystems;

public class ProB_InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("main")]
    [SerializeField] private ProB_ItemSlot _itemSlot;
    [SerializeField] private Transform _parentBeforeDrag;

    [Header("const")]
    [SerializeField] private Canvas _mainParent;
    [SerializeField] private RectTransform _rectTransform;

    public ProB_ItemSlot ItemSlot => _itemSlot;


    private void Start()
    {
        _mainParent = GM_ProBuilder.Instance.UIManager.GameCanvas;
        _itemSlot?.UpdateSlot();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_itemSlot.ItemData == null) return; // no item inside slot

        _parentBeforeDrag = this.transform;
        _itemSlot.transform.SetParent(_mainParent.transform);

        _itemSlot.CanvasGroup.alpha = 0.4f;
        _itemSlot.CanvasGroup.blocksRaycasts = false;

        eventData.Use(); // bol'she ne buget iskat' drugih obj pod nim
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_itemSlot.ItemData == null) return;

        // correct transformation for dragging // prosto podrugomu shitaet gde slot
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
            );

        _itemSlot.transform.position = _rectTransform.TransformPoint(localPoint);

        /*
         * proshloe
        _itemSlot.transform.position = eventData.position;
        */
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_itemSlot.ItemData == null) return;

        //bool isWasDroppedOnSlot =
        //    eventData.pointerCurrentRaycast.gameObject.TryGetComponent<ProB_InventorySlot>(out var targetSlot);
        //print(eventData.pointerCurrentRaycast);
        //print(isWasDroppedOnSlot);
        //print(targetSlot);

        _itemSlot.CanvasGroup.alpha = 1f;
        _itemSlot.CanvasGroup.blocksRaycasts = true;

        if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<ProB_InventorySlot>(out var targetSlot) 
            && targetSlot != this
            )
        {
            SwapItems(targetSlot);
            print("dropped");
        }
        else
        {
            ReturnItemToOriginalSlot();
            print("returned");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_itemSlot.ItemData == null) return;

        ProB_UIManager.ClickItemSlot?.Invoke(_itemSlot.ItemData);
    }

    private void SwapItems(ProB_InventorySlot targetSlot)
    {
        var tempSlot = targetSlot.ItemSlot;
        targetSlot._itemSlot = this._itemSlot;
        this._itemSlot = tempSlot;

        //if (this._itemSlot.ItemData != null)
            this._itemSlot.transform.SetParent(this.transform);

        //if (targetSlot._itemSlot.ItemData != null)
            targetSlot._itemSlot.transform.SetParent(targetSlot.transform);
    }

    private void ReturnItemToOriginalSlot()
    {
        _itemSlot.transform.SetParent(_parentBeforeDrag);
    }
}
