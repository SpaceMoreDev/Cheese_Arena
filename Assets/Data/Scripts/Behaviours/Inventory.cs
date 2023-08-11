using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.AssetImporters;

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

        private GameObject _prefap;
        
        public Inventory currentInventory;

        
        public ItemObject Data {
            get => _data; 
        }

        public GameObject Prefap {
            // the prefap location to be able to spawn the UI items.
            get => Resources.Load<GameObject>("Prefaps/UI/Inventory/InventoryItem");
        }

        public Item(ItemObject itemObject, Inventory inventory)
        {
            _data = itemObject;
            currentInventory = inventory;
            ItemEffects = new Effects(itemObject.Effect);
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
        public List<GameObject> UIObjects = new();
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

        public Inventory(List<ItemObject> items)
        {
            List<Item> itemList = new();
            foreach(ItemObject i in items)
            {
                Item item = new(i, this);
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
            InventoryItems.Add(item);
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
            Item item = Item.FindWithID(ID, this);
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
        public void UpdateMenuItems()
        {
            RemoveItemsToMenu();
            foreach(var i in this.InventoryItems){
                GameObject spawnedItem = GameObject.Instantiate<GameObject>(i.Prefap, Panel.transform);
                spawnedItem.GetComponent<UnityEngine.UI.Image>().sprite = i.Data.Sprite;
                spawnedItem.GetComponent<DragItems>().itemData = i;
                i.currentInventory = this;
                UIObjects.Add(spawnedItem);
            }
        }

        public void TransferItem(Item item, Inventory newInventory)
        {
            this.RemoveFromInventory(item);
            newInventory.AddToInventory(item);
            item.currentInventory = newInventory;

            Debug.Log($"items A = {InventoryItems.Count}, items B {newInventory.InventoryItems.Count}");

            this.UpdateMenuItems();
            newInventory.UpdateMenuItems();
        }
    }
}