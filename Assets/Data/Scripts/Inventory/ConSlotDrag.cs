using System.Collections;
using System.Collections.Generic;
using Behaviours;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConSlotDrag : MonoBehaviour, IDropHandler
{
    internal ConsumeSlot consumeSlot;
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null && eventData.pointerDrag != gameObject)
        {
            Item item = eventData.pointerDrag.GetComponent<DragItems>().itemData;
            Inventory currentInventory = item.currentInventory;

            // currentInventory.TransferItem(item, this.consumeSlot.Inventory);
            // currentInventory.UpdateMenuItems();

            currentInventory.RemoveFromInventory(item);
            if(consumeSlot.Item != null){
                currentInventory.AddToInventory(consumeSlot.Item);
            }
            currentInventory.UpdateMenuItems();
            

            ConsumeSlot tobereplaced = this.consumeSlot;
            
            if(eventData.pointerDrag.TryGetComponent<ConSlotDrag>(out ConSlotDrag slotdrag)){
                if(consumeSlot.Item == null)
                {
                    slotdrag.consumeSlot.ReplaceWithEmpty();
                }
                else
                {
                    ConsumeSlot temp = slotdrag.consumeSlot;
                    Inventory tempinv = slotdrag.consumeSlot.Inventory;
                    Item tempItem = slotdrag.consumeSlot.Item;

                    ConsumeItems.ReplaceSlot(ref slotdrag.consumeSlot, ref tobereplaced);
                    ConsumeItems.ReplaceSlot(ref tobereplaced, ref temp);

                    temp.Inventory = tobereplaced.Inventory;
                    tobereplaced.Inventory = tempinv;

                    slotdrag.consumeSlot.Item = tobereplaced.Item;
                    tobereplaced.Item = tempItem;

                }

                Destroy(eventData.pointerDrag.GetComponent<DragItems>()._draggableObject);

            }else{
                tobereplaced.UIobject.GetComponent<Image>().sprite = item.Data.Sprite;
                tobereplaced.UIobject.GetComponent<Image>().color = Color.white;
                tobereplaced.Quantity = 1;
                tobereplaced.UIobject.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
                
                tobereplaced.Item = item;
                item.currentInventory = tobereplaced.Inventory;

                Destroy(eventData.pointerDrag);
            }
            ConsumeItems.Sort(ConsumeItems._consumeSlots);
            Debug.Log($"moved {item.Data.ItemName} into consume inventory");
        }
    }
}
