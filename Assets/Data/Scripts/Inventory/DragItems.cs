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
IDropHandler,
IPointerUpHandler
{
    public Slot DragData;

    private CanvasGroup canvasGroup;
    bool draggable = true;
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
        if(DragData.Item == null || DragData.Item.Data == null){Debug.Log("Empty slot!"); return;}

        if(DragData.isConsumable){
            Debug.Log("begin drag consumable");
            _draggableObject = Instantiate(_itemSlotPrefap, parent.parent);

            _draggableObject.GetComponent<Image>().sprite = imageCom.sprite;

            imageCom.sprite = DragData.Item.currentInventory.SlotPrefap.GetComponent<Image>().sprite;
            DragData._quantityLabel = 0;
            imageCom.color = Color.gray;
        }
        else{   
            
            Debug.Log("begin drag normal item");
            _draggableObject = Instantiate(_itemSlotPrefap, parent.parent.parent);

            _draggableObject.GetComponent<Image>().sprite = imageCom.sprite;

            canvasGroup.alpha = 0;
        }
        canvasGroup.blocksRaycasts = false;
        _draggableObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (50, 50);
        _draggableObject.GetComponent<Image>().color = new Color(255,255,255,0.6f);
       
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {       
        if(DragData.Item.Data == null){return;}
        if(_draggableObject == null){return;}

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
        if(DragData.Item == null || DragData.Item.Data == null){Debug.Log("Empty slot!"); return;}
        Debug.Log($"=> {DragData.Item.Data.Description}");
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if(_draggableObject != null){
            GameObject.Destroy(_draggableObject);
            Debug.Log("ended drag");
        }

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {   
        GameObject dragObject = eventData.pointerDrag;
        if(dragObject.TryGetComponent<DragItems>(out DragItems drag))
        {
            if(drag.DragData.Item.Data == null){return;}
            
            DragData.DragOn(ref drag.DragData);
        }
    }
}
