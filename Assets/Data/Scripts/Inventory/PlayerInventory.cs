using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using Behaviours;
using System.Linq;

public class PlayerInventory : MonoBehaviour
{

    // --- Player inventory handler ---
    [SerializeField] internal GameObject _inventorySlots;
    [SerializeField] internal GameObject _inventoryMenu;
    [SerializeField] private GameObject _consumableSlots;

    [Space]
    //---
    [SerializeField] private List<ItemObject> playerItems;
    private GameObject _inventoryItemPrefap;
    private bool toggleInventory = false;
    private Inventory _inventory;

    public  Inventory Inventory {
        get => _inventory;
    }

    public static PlayerInventory Player;
    private void Awake() {
        Player = this;
        _inventory = new Inventory(playerItems, _inventorySlots);
        Inventory.Panel = _inventorySlots;
        _inventoryMenu.gameObject.SetActive(toggleInventory);

        InputManager.inputActions.General.Inventory.started += _ => Toggle();
        InputManager.inputActions.UI.Inventory.started += _ => Toggle();
        Inventory.Panel.transform.parent.GetComponent<DragToInventory>()._inventory = Inventory;
    }

    private void Toggle()
    {
        toggleInventory = !toggleInventory;
        _inventoryMenu.gameObject.SetActive(toggleInventory);
        
        if(toggleInventory){
            InputManager.ToggleActionMap(InputManager.inputActions.UI);
            Inventory.UpdateMenuItems();
            Cursor.lockState = CursorLockMode.None;
            PlayerCameraHandler.Instance.SetCamerPOV(false);
        }
        else {
            InputManager.ToggleActionMap(InputManager.inputActions.General);
            // Inventory.RemoveItemsToMenu();
            Cursor.lockState = CursorLockMode.Locked;
            PlayerCameraHandler.Instance.SetCamerPOV(true);
        }
    }
}