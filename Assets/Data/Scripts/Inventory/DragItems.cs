using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Behaviours;
using Unity.Mathematics;
using MyBox;

public class DragItems : MonoBehaviour,
IPointerDownHandler,
IBeginDragHandler, 
IEndDragHandler, 
IDragHandler,
IPointerUpHandler
{
    public Item itemData;

    private CanvasGroup canvasGroup;
    
    Canvas canvas;
    Image imageCom;
    RectTransform rectTransform;
    private Transform parent;
    private static GameObject _itemSlotPrefap{
        get => Resources.Load<GameObject>("Prefaps/UI/Inventory/DragItem");
    }
    public GameObject _draggableObject;
    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponent<Image>().canvas;
        canvasGroup = GetComponent<CanvasGroup>();
        imageCom = GetComponent<Image>();
        parent = transform.parent;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {

        if(eventData.pointerDrag.TryGetComponent<ConSlotDrag>(out ConSlotDrag drag)){
            _draggableObject = Instantiate(_itemSlotPrefap, parent.parent);
            _draggableObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (50, 50);
            _draggableObject.GetComponent<Image>().sprite = imageCom.sprite;
            itemData = drag.consumeSlot.Item;
            
            drag.consumeSlot.UIobject.GetComponent<Image>().sprite = ConsumeSlot._emptySlotPrefap.GetComponent<Image>().sprite;
            drag.consumeSlot.UIobject.GetComponent<Image>().color = Color.gray;
        }
        else{   
            _draggableObject = Instantiate(_itemSlotPrefap, parent.parent.parent);
            _draggableObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (50, 50);
            _draggableObject.GetComponent<Image>().sprite = imageCom.sprite;

            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }

    
        // _draggableObject.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
        _draggableObject.GetComponent<Image>().color = new Color(255,255,255,0.6f);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {   
        // _draggableObject.GetComponent<RectTransform>().anchoredPosition += 
        // eventData.delta/_draggableObject.GetComponent<Image>().canvas.scaleFactor;
        Vector2 movePos = Vector2.zero;
        Canvas parentCanvas = _draggableObject.GetComponent<Image>().canvas;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        parentCanvas.transform as RectTransform,
        PlayerInputHandler.MouseVector, parentCanvas.worldCamera,
        out movePos);

        _draggableObject.transform.position = parentCanvas.transform.TransformPoint(movePos);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if(_draggableObject != null){
        GameObject.Destroy(_draggableObject);
        Debug.Log("ended drag");
        }
        
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if(TryGetComponent<ConSlotDrag>(out ConSlotDrag drag))
        {
            Debug.Log($"=> {drag.consumeSlot.Item.Data.Description}");
        }
        else{
            Debug.Log($"=> {itemData.Data.Description}");
        }
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if(_draggableObject != null){
        GameObject.Destroy(_draggableObject);
        Debug.Log("ended drag");
        }
    }
}
