using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Managers;
using Behaviours;

public class Loot : MonoBehaviour, ActivateActions
{
    [SerializeField] private List<ItemObject> _inventoryItems;
    private GameObject _displayUI;
    private bool _activated = false;
    private Animator anim;
    private Transform _inventorySlots;
    private Transform _playerSlots;
    private Inventory _inventory;

    public GameObject DisplayUI { get => _displayUI; set => _displayUI = value; }
    public bool Activated { get => _activated;}
    public Inventory Inventory{
        get{
            if(_inventory == null){
                _inventory = new Inventory(_inventoryItems);
            }
            return _inventory;
        }
    }
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        GameObject text = Resources.Load<GameObject>("Prefaps/UI/InteractText");

        Vector3 spawnPosition = new(0,1,0);
        _displayUI = Instantiate(text,transform);
        _displayUI.transform.position += spawnPosition; // for "press E to interact" text.
    }

    public void Activate()
    {
        Inventory.Panel = PlayerInventory.Player._inventoryMenu.transform.GetChild(1).GetChild(1).gameObject;

        if(!_activated){ 
            anim.Play("Open");
            Cursor.lockState = CursorLockMode.None;
            Inventory.Panel.transform.parent.GetComponent<DragToInventory>()._inventory = Inventory;

            InputManager.ToggleActionMap(InputManager.inputActions.UI);
            Inventory.UpdateMenuItems();
            PlayerInventory.Player.Inventory.UpdateMenuItems();;

            _activated= true;
            _displayUI.SetActive(false);
            Inventory.Panel.transform.parent.gameObject.SetActive(true);
            PlayerInventory.Player._inventoryMenu.SetActive(true);
            PlayerCameraHandler.Instance.SetCamerPOV(false);

        }else{
            InputManager.ToggleActionMap(InputManager.inputActions.General); 
            Cursor.lockState = CursorLockMode.Locked;
            _activated= false;
            _displayUI.SetActive(true);
            Inventory.Panel.transform.parent.gameObject.SetActive(false);
            PlayerInventory.Player._inventoryMenu.SetActive(false);
            PlayerCameraHandler.Instance.SetCamerPOV(true);

        }
    }
}