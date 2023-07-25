using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Item
{

    private int _ID;
    private string _itemName;
    private string _description;
    private Sprite _sprite;
    private ItemObject _itemObject;

    public string ItemName {
        get => _itemName; 
        set => _itemName = value;
    }

    public string Description {
        get => _description; 
        set => _description = value;
    }

    public Sprite ItemSprite {
        get => _sprite; 
        set => _sprite = value;
    }

    public ItemObject ItemObject {
        get => _itemObject; 
    }

    public int ID {
        get => _ID; 
    }

    public Item(ItemObject itemObject)
    {
        itemObject = _itemObject;
        _itemName = itemObject.ItemName;
        _sprite = itemObject.Sprite;
        _description = itemObject.Description;
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
}

public class Inventory
{
    public List<Item> InventoryItems;

    public Inventory()
    {
        InventoryItems = new List<Item>();
    }

    public Inventory(List<Item> items)
    {
        InventoryItems = items;
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
