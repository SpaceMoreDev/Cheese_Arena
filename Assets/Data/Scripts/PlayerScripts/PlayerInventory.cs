using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Behaviours;

public class PlayerInventory : MonoBehaviour
{   
    private Inventory _inventory;
    public Inventory Inventory {
        get{
            if(_inventory == null)
            {
                return new Inventory();
            }
            return _inventory;
        }
    }

    public void AddToInventory(Item item) => Inventory.AddToInventory(item);
    public void RemoveFromInventory(Item item) => Inventory.RemoveFromInventory(item);
}