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
        GameObject data = eventData.pointerDrag;
        
        if(data.TryGetComponent<DragItems>(out DragItems slot))
        {
            if(slot.DragData.Item.Data == null){return;}

            

            Slot itemSlot = slot.DragData;
            Inventory currentInventory = itemSlot.Item.currentInventory;
            
            if(slot.DragData.isConsumable){
                ConsumeItems.current.RemoveItemToConsume(slot.DragData, _inventory);
                ConsumeItems.current.Inventory.UpdateMenuItems();
                return;
            }
            if(_inventory != currentInventory)
            {
               
                currentInventory.TransferItem(itemSlot,_inventory); //to do
                Debug.Log($"moved {itemSlot.Item.Data.ItemName} into another inventory");
            }
        }       
    }
}
