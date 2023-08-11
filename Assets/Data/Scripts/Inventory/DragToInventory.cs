using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Behaviours;
using UnityEngine;
using UnityEngine.EventSystems;

// on the dragged to location
// will add items or replace items in inventory slots.
public class DragToInventory : MonoBehaviour, IDropHandler
{
    internal Inventory _inventory;

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            DragItems dragItems;
            GameObject data = eventData.pointerDrag;
            if(data.TryGetComponent<ConSlotDrag>(out ConSlotDrag con_slot))
            {
                Item item = con_slot.consumeSlot.Item;
                Inventory currentInventory = con_slot.consumeSlot.Inventory;
                
                
                if(_inventory != currentInventory)
                {
                    currentInventory.TransferItem(item, _inventory);

                    con_slot.consumeSlot.Inventory.RemoveFromInventory(con_slot.consumeSlot.Item);
                    con_slot.consumeSlot.ReplaceWithEmpty();
                    Debug.Log($"moved {item.Data.ItemName} into another inventory");
                }
            }
            else if(data.TryGetComponent<DragItems>(out DragItems slot))
            {
                Item item = slot.itemData;
                Inventory currentInventory = item.currentInventory;
                
                if(_inventory != currentInventory)
                {
                    currentInventory.TransferItem(item, _inventory);
                    Debug.Log($"moved {item.Data.ItemName} into another inventory");
                }
                dragItems = slot;
                GameObject.Destroy(dragItems._draggableObject);
            }

            

        }
    }
}
