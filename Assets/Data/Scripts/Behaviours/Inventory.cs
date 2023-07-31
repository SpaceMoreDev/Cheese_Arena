using System.Collections.Generic;
using System;

public enum Options{
    Use,
    Remove,
    Eat,
    SendToSlots
}
namespace Behaviours
{
    [Serializable]
    public class Item
    {
        private ItemObject _data;
        private int _quantity = 1;
        private Effects ItemEffects;

        public int Quantity {
            get => _quantity; 
        }
        public ItemObject Data {
            get => _data; 
        }

        public Item(ItemObject itemObject)
        {
            _data = itemObject;
            ItemEffects = new Effects(itemObject.Effect);
        }

        public void AddToQuantity()
        {
            _quantity ++;
        }

        public void RemoveFromQuantity()
        {
            _quantity --;
        }

        public static Item FindItemWithID(int id)
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

        public void Use(UnityEngine.GameObject target)
        {
            ItemEffects.Activate(Data.EffectValue, target);

        }
    }

    public class Inventory
    {
        public List<Item> InventoryItems = new();
        

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

        ~Inventory()
        {
            //destructor
        }

        public void inspectMenu(Options option, Item item)
        {
            UnityEngine.Debug.Log($"inspected {item.Data.ItemName}");

            // switch(option)
            // {
            //     case Options.Use:
            //         break;
            //     case Options.Remove:
            //         break;
            //     case Options.Eat:
            //         break;
            //     case Options.SendToSlots:
            //         break;
            // }
        }

        public void AddToInventory(Item item)
        {
            InventoryItems.Add(item);
        }
        
        public void RemoveFromInventory(Item item)
        {
            InventoryItems.Remove(item);
        }
        
        void AddByID(int ID)
        {
            Item item = Item.FindItemWithID(ID);
            AddToInventory(item);
        }
    }
}