using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using Behaviours;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ItemObject> playerItems;

    private bool toggleInventory = false;
    private PlayerInventoryManager _inventoryManager;
    private Inventory _inventory;


    public Inventory Inventory {
        get{
            if(_inventory == null){
                return new Inventory(playerItems);
            }
            return _inventory;
        }
    }

    private void Awake() {
        _inventoryManager = PlayerInventoryManager.Instance;
        _inventoryManager.InventoryMenu.gameObject.SetActive(toggleInventory);

        InputManager.inputActions.General.Inventory.started += _ => Toggle();
        InputManager.inputActions.UI.Inventory.started += _ => Toggle();
    }

    private void Toggle()
    {
        toggleInventory = !toggleInventory;
        _inventoryManager.InventoryMenu.gameObject.SetActive(toggleInventory);
        
        if(toggleInventory){
            InputManager.ToggleActionMap(InputManager.inputActions.UI);      
            _inventoryManager.UpdateMenuItems(Inventory);
            PlayerCameraHandler.Instance.SetCamerPOV(false);
        }
        else {
            InputManager.ToggleActionMap(InputManager.inputActions.General);
            _inventoryManager.RemoveItemsToMenu();
            PlayerCameraHandler.Instance.SetCamerPOV(true);
        }
    }
    
    public void AddToInventory(Item item) 
    {
        if (Inventory.InventoryItems.Count < _inventoryManager.maxItems){
            Inventory.AddToInventory(item);
            _inventoryManager.UpdateMenuItems(Inventory);
            return;
        }
        Debug.Log("No space in the inventory");
    }

    public void RemoveFromInventory(Item item)
    {
        Inventory.RemoveFromInventory(item);
    }
}