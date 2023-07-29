using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Managers;

public class Chest : MonoBehaviour, ActivateActions
{
    [SerializeField] private List<ItemObject> _inventoryItems;

    private GameObject _displayUI;
    private bool _activated = false;
    private Animator anim;

    public GameObject DisplayUI { get => _displayUI; }
    public bool Activated { get => _activated;}
    
    private InventoryManager _inventoryManager;
    private Inventory _inventory;
    public Inventory Inventory{
        get{
            if(_inventory == null){
                return new Inventory();
            }
            return _inventory;
        }
    }
    

    private void Awake()
    {
        _inventoryManager = InventoryManager.Instance;
        anim = GetComponent<Animator>();

        GameObject resource = Resources.Load<GameObject>("Prefaps/UI/InteractText");
        Vector3 spawnPosition = new(0,1,0);
        _displayUI = Instantiate(resource,transform);
        
        _displayUI.transform.position += spawnPosition;
    }

    public void Activate()
    {
        anim.Play("Open");

        if(!_activated){
            InputManager.ToggleActionMap(InputManager.inputActions.UI);
            _inventoryManager.AddItemsToMenu(_inventoryItems);

            _activated= true;
            _displayUI.SetActive(false);
        }else{
            InputManager.ToggleActionMap(InputManager.inputActions.General); 
            _inventoryManager.RemoveItemsToMenu();

            _activated= false;
            _displayUI.SetActive(true);
        }
        
        _inventoryManager.InventorySlots.SetActive(true);
    }
}