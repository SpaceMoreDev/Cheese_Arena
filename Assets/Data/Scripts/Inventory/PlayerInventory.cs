using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using Behaviours;

public class PlayerInventory : MonoBehaviour
{

    // --- Player inventory handler ---
    [SerializeField] internal GameObject _inventorySlots;
    [SerializeField] private GameObject _inventoryMenu;
    [SerializeField] private GameObject _consumableSlots;

    [Space]
    //---
    [SerializeField] private List<ItemObject> playerItems;
    private GameObject _inventoryItemPrefap;
    private bool toggleInventory = false;
    public Inventory _inventory;
    [SerializeField] public int MaxItems {
        get => _inventory.maxItems;
        set => _inventory.maxItems = value;
    }

    public  Inventory Inventory {
        get{
            if(_inventory == null){
                _inventory = new Inventory(playerItems);
            }
            return _inventory;
        }
    }

    public static PlayerInventory Player;
    private void Awake() {
        Player = this;
        _inventoryMenu.gameObject.SetActive(toggleInventory);

        InputManager.inputActions.General.Inventory.started += _ => Toggle();
        InputManager.inputActions.UI.Inventory.started += _ => Toggle();
    }

    private void Toggle()
    {
        toggleInventory = !toggleInventory;
        _inventoryMenu.gameObject.SetActive(toggleInventory);
        
        if(toggleInventory){
            InputManager.ToggleActionMap(InputManager.inputActions.UI);      
            Inventory.UpdateMenuItems(_inventorySlots);
            PlayerCameraHandler.Instance.SetCamerPOV(false);
        }
        else {
            InputManager.ToggleActionMap(InputManager.inputActions.General);
            Inventory.RemoveItemsToMenu();
            PlayerCameraHandler.Instance.SetCamerPOV(true);
        }
    }
}