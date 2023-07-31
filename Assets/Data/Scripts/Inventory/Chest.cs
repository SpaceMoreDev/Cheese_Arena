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
    private ChestInventory _chestInventory;

    public GameObject DisplayUI { get => _displayUI; }
    public bool Activated { get => _activated;}
    
    private Inventory _inventory;
    public Inventory Inventory{
        get{
            if(_inventory == null){
                return new Inventory(_inventoryItems);
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
        _inventoryUI = Instantiate<Canvas>(invUI, transform);
        _chestInventory = _inventoryUI.GetComponent<ChestInventory>();
        
        _displayUI.transform.position += spawnPosition;
        
    }

    public void Activate()
    {
        anim.Play("Open");

        if(!_activated){
            InputManager.ToggleActionMap(InputManager.inputActions.UI);
            _chestInventory.UpdateMenuItems(Inventory);

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