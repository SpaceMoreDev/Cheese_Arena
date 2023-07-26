using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using Behaviours;

public class PlayerInventory : MonoBehaviour
{   
    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private GameObject _inventoryItemPrefap;
    [SerializeField] private GameObject _inventorySlots;
    [SerializeField] private GameObject _consumableSlots;

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

    private void Awake() {
        _inventoryItemPrefap = Resources.Load<GameObject>("Prefaps/UI/Inventory/InventoryItem");
        InputManager.inputActions.General.Inventory.started +=_ => {
            InputManager.ToggleActionMap(InputManager.inputActions.UI);
        };
    }

    public void AddToInventory(Item item) => Inventory.AddToInventory(item);
    public void RemoveFromInventory(Item item) => Inventory.RemoveFromInventory(item);
}