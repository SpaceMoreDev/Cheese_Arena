using System.Collections.Generic;
using UnityEngine;
using System;

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
        I_item Clone();
        void Use(GameObject target);
    }

    [Serializable]
    public class Item : I_item
    {
        private ItemObject _data;
        
        private Effects ItemEffects;

        private GameObject _prefap;

        
        public ItemObject Data {
            get => _data; 
        }

        public GameObject Prefap {
            // the prefap location to be able to spawn the UI items.
            get => Resources.Load<GameObject>("Prefaps/UI/Inventory/InventoryItem");
        }

        public Item(ItemObject itemObject)
        {
            _data = itemObject;
            ItemEffects = new Effects(itemObject.Effect);
        }


        public static Item FindWithID(int id)
        {
            // for making sure that no item has the same ID.
            for(int i=0 ; i< ItemObject.Allitems.Count ; i++)
            {
                if(id == ItemObject.Allitems[i].ID)
                {
                    return new Item( ItemObject.Allitems[i] );
                }
            }
            return null;
        }

        public void Use(GameObject target)
        {
            ItemEffects.Activate(Data.EffectValue, target);
        }

        public I_item Clone() // factory design pattern
        {
            Item newItem =  new Item(this._data);
            return newItem;
        }
    }

    public class Inventory
    {
        public List<Item> InventoryItems = new();
        public List<GameObject> UIObjects = new();
        public int maxItems = 5;
        public Inventory()
        {
            InventoryItems = new List<Item>();
        }

        public Inventory(List<Item> items)
        {
            InventoryItems = items;
        }

        public Inventory(List<ItemObject> items)
        {
            List<Item> itemList = new();
            foreach(ItemObject i in items)
            {
                Item item = new(i);
                itemList.Add(item);
            }

            InventoryItems = itemList;
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
        public void AddToInventory(Item item)
        {
            if(InventoryItems.Count < maxItems) { InventoryItems.Add(item); return; }
            Debug.Log("No space in the inventory");
        }
        /// <summary>
        /// Remove an item from list of items.
        /// </summary>
        /// <param name="item">Item to be reomved.</param>
        public void RemoveFromInventory(Item item)
        {
            InventoryItems.Remove(item);
        }
        /// <summary>
        /// Add Item to inventory by ID.
        /// (call UpdateMenuItems() after)
        /// </summary>
        /// <param name="ID">The ID of the item to be added.</param>
        void AddByID(int ID)
        {
            Item item = Item.FindWithID(ID);
            AddToInventory(item);
        }

        /// <summary>
        /// Destroy UI items in list to clear the menu.
        /// </summary>
        public void RemoveItemsToMenu()
        {
            foreach(GameObject i in UIObjects){
                GameObject.Destroy(i);
            }
            UIObjects.Clear();
        }
        /// <summary>
        /// Update UI item instances.
        /// </summary>
        /// <param name="InventorySlots"> The inventory slots where the UI icons will be put in.</param>
        public void UpdateMenuItems(GameObject InventorySlots)
        {
            RemoveItemsToMenu();
            foreach(var i in this.InventoryItems){
                GameObject spawnedItem = GameObject.Instantiate<GameObject>(i.Prefap, InventorySlots.transform);
                spawnedItem.GetComponent<UnityEngine.UI.Image>().sprite = i.Data.Sprite;
                UIObjects.Add(spawnedItem);
                Debug.Log($"the list has {UIObjects.Count} items");
            }
        }
    }
}