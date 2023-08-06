using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Managers;
using Behaviours;

public class Chest : MonoBehaviour, ActivateActions
{
    [SerializeField] private List<ItemObject> _inventoryItems;

    private GameObject _displayUI;
    private Canvas _inventoryUI;
    private bool _activated = false;
    private Animator anim;

    private GameObject _inventorySlots;
    private GameObject _playerSlots;

    public GameObject DisplayUI { get => _displayUI; }
    public bool Activated { get => _activated;}
    
    private Inventory _inventory;
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
        Canvas invUI = Resources.Load<Canvas>("Prefaps/UI/Inventory/ChestInventory");


        Vector3 spawnPosition = new(0,1,0);
        _displayUI = Instantiate(text,transform);
        _displayUI.transform.position += spawnPosition; // for "press E to interact" text.


        _inventoryUI = Instantiate<Canvas>(invUI, transform);
        _inventorySlots = _inventoryUI.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
        _playerSlots = _inventoryUI.transform.GetChild(1).GetChild(0).GetChild(1).gameObject;

    }

    public void Activate()
    {
        anim.Play("Open");

        if(!_activated){
            InputManager.ToggleActionMap(InputManager.inputActions.UI);
            Inventory.UpdateMenuItems(_inventorySlots);
            PlayerInventory.Player.Inventory.UpdateMenuItems(_playerSlots);;

            _activated= true;
            _displayUI.SetActive(false);
            _inventoryUI.gameObject.SetActive(true);
            PlayerCameraHandler.Instance.SetCamerPOV(false);

        }else{
            InputManager.ToggleActionMap(InputManager.inputActions.General); 

            _activated= false;
            _displayUI.SetActive(true);
            _inventoryUI.gameObject.SetActive(false);
            PlayerCameraHandler.Instance.SetCamerPOV(true);

        }
    }
}