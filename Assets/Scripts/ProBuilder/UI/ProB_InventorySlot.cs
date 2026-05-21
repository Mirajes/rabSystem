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
    [SerializeField] private CanvasGroup _canvasGroup;

    public ProB_ItemSlot ItemSlot => _itemSlot;


    private void Start()
    {
        _mainParent = GM_ProBuilder.Instance.UIManager.GameCanvas;
        _itemSlot?.UpdateSlot();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_itemSlot.ItemData == null) return;

        _parentBeforeDrag = this.transform;
        _itemSlot.transform.SetParent(_mainParent.transform);

        _canvasGroup.alpha = 0.4f;
        _canvasGroup.blocksRaycasts = false;

        eventData.Use(); // bol'she ne buget iskat' drugih obj pod nim
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_itemSlot.ItemData == null) return;

        // correct transformation for dragging
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

        if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<ProB_InventorySlot>(out var targetSlot) 
            //&& targetSlot != this)
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

        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_itemSlot.ItemData == null) return;

        ProB_UIManager.ClickItemSlot?.Invoke(_itemSlot.ItemData);
    }

    private void SwapItems(ProB_InventorySlot targetSlot)
    {
        var tempItem = targetSlot.ItemSlot;
        targetSlot._itemSlot = this._itemSlot;
        this._itemSlot = tempItem;

        if (this._itemSlot.ItemData != null)
            this._itemSlot.transform.SetParent(targetSlot.transform);

        if (targetSlot._itemSlot.ItemData != null)
            targetSlot._itemSlot.transform.SetParent(targetSlot.transform);
    }

    private void ReturnItemToOriginalSlot()
    {
        _itemSlot.transform.SetParent(_parentBeforeDrag);
    }
}
