using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.VisualScripting;

public enum Options{
    Use,
    Remove,
    Eat,
    SendToSlots
}



namespace Behaviours
{
    public interface I_item
    {
        I_item Clone(Inventory inventory);
        void Use(GameObject target);
    }

    [Serializable]
    public class Item : I_item
    {
        private ItemObject _data;
        
        private Effects ItemEffects;
        
        public Inventory currentInventory;

        public ItemObject Data {
            get => _data; 
            set=> _data = value;
        }
        
        public Item(ItemObject itemObject, Inventory inventory)
        {
            _data = itemObject;
            currentInventory = inventory;
            ItemEffects = new Effects(itemObject.Effect);
        }
        public Item(Inventory inventory)
        {
            _data = null;
            currentInventory = inventory;
            ItemEffects = new Effects(EffectTypes.Empty);
        }


        public static Item FindWithID(int id, Inventory inventory)
        {
            // for making sure that no item has the same ID.
            for(int i=0 ; i< ItemObject.Allitems.Count ; i++)
            {
                if(id == ItemObject.Allitems[i].ID)
                {
                    return new Item( ItemObject.Allitems[i], inventory);
                }
            }
            return null;
        }

        public void Use(GameObject target)
        {
            ItemEffects.Activate(Data.EffectValue, target);
        }

        public I_item Clone( Inventory inventory) // factory design pattern
        {
            Item newItem =  new Item(this._data, inventory);
            return newItem;
        }
    }

    public class Inventory
    {
        public List<Item> InventoryItems = new();
        public List<Slot> itemSlots = new();
        public GameObject Panel;
        public int MaxInventoryItems = 10;


        public Inventory()
        {
            InventoryItems = new List<Item>();
        }

        public Inventory(List<Item> items)
        {
            InventoryItems = items;
        }

        public Inventory(List<ItemObject> items, GameObject UIpanel)
        {
            List<Item> itemList = new();
            foreach(ItemObject i in items)
            {
                Item item = new(i, this);
                itemList.Add(item);
            }
            Panel = UIpanel;
            InventoryItems = itemList;
            bool consumable = this == ConsumeItems.current.Inventory;
            AddSlotsToMenu(consumable);
        }
        //Class Destructor
        ~Inventory()
        {
            InventoryItems.Clear();
            RemoveItemsToMenu();
        }
        /// <summary>
        /// Add an item to the list of items.
        /// </summary>
        /// <param name="item"></param>
        public void AddToInventory(Slot slot)
        {
            Item item = slot.Item;
            item.currentInventory = this;
            this.InventoryItems.Add(item);
            this.AddMenuItem(slot);
        }

        /// <summary>
        /// Remove an item from list of items.
        /// </summary>
        /// <param name="item">Item to be reomved.</param>
        public void RemoveFromInventory(Slot slot)
        {
            Item item = slot.Item;
            this.InventoryItems.Remove(item);
            this.RemoveItemFromMenu(slot);
        }

        /// <summary>
        /// Add Item to inventory by ID.
        /// (call UpdateMenuItems() after)
        /// </summary>
        /// <param name="ID">The ID of the item to be added.</param>
        void AddByID(int ID)
        {
            Item item = Item.FindWithID(ID, this);
            Slot slot = new Slot(InventoryItems.Count, Panel.transform, item);
            AddToInventory(slot);
        }

        /// <summary>
        /// Destroy UI items in list to clear the menu.
        /// </summary>
        public void RemoveItemsToMenu()
        {
            foreach(Slot i in itemSlots){
                GameObject.Destroy(i.MenuObject);
            }
            // itemSlots.Clear();
        }

        public void RemoveItemFromMenu(Slot item)
        {
            foreach(Slot i in itemSlots){
                if (i.Item == item.Item)
                {
                    GameObject.Destroy(i.MenuObject);
                    break;
                }
            }
            itemSlots.Remove(item);
        }

        /// <summary>
        /// Adds Slot's GameObject to objects list.
        /// </summary>
        /// <param name="slot">Item Slot</param>
        public void AddMenuItem(Slot slot)
        {
            this.itemSlots.Add(slot);
        }

        /// <summary>
        /// This function will add the inventory items to the inventory (like refresh).
        /// </summary>
        public void AddSlotsToMenu(bool isConsumable)
        {
            int counter = 2;
            foreach(var i in InventoryItems){
                
                Slot spawnedSlot = new(
                    counter,
                    Panel.transform,
                    i,
                    isConsumable
                );

                AddMenuItem(spawnedSlot);

                counter++;
            }
        }

        /// <summary>
        /// Update UI item instances.
        /// </summary>
        /// <param name="InventorySlots"> The inventory slots where the UI icons will be put in.</param>
        public void UpdateMenuItems()
        {   RemoveItemsToMenu();
            this.itemSlots.OrderBy(x => x.Number);
            foreach(Slot i in this.itemSlots)
            {
                if(i.isConsumable){
                    i.MenuObject = GameObject.Instantiate(Slot._consumablePrefap,Panel.transform);
                }
                else{
                    i.MenuObject = GameObject.Instantiate(Slot._emptySlotPrefap,Panel.transform);
                }

                i.UpdateSlot();
            }
        }

        public void TransferItem(Slot itemSlot, Inventory newInventory)
        {
            this.RemoveFromInventory(itemSlot);
            newInventory.AddToInventory(itemSlot);
        
            Debug.Log($"item {itemSlot.Item.Data.ItemName} moved to a {newInventory.InventoryItems.Count} items inventory");
            this.UpdateMenuItems();
            newInventory.UpdateMenuItems();
        }
    }
}