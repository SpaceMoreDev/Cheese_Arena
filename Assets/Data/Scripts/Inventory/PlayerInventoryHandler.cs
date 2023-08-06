using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    
    public class PlayerInventoryHandler : MonoBehaviour //todo remove this
    {
        [SerializeField] public  PlayerInventory PlayerInventory;
        [SerializeField] private GameObject _inventoryItemPrefap;
        [SerializeField] private GameObject _inventorySlots;
        [SerializeField] private GameObject _inventoryMenu;
    
        private GameObject _consumableSlots;
        public int maxItems = 10;
        private static PlayerInventoryHandler _instance;
        public List<GameObject> ObjectList;
        public InventoryManager InventoryManager;

        [HideInInspector] public GameObject InventorySlots { 
            get => _inventorySlots; 
        }

        [HideInInspector] public GameObject InventoryMenu { 
            get => _inventoryMenu; 
        }

        [HideInInspector] public GameObject ConsumableSlots { 
            get => _consumableSlots; 
        }

        [HideInInspector] public GameObject InventoryItemPrefap{
            get=> _inventoryItemPrefap; 
        }

        public static PlayerInventoryHandler Instance {
            get => _instance;
        }
        
        public PlayerInventoryHandler()
        {
            if(_instance == null){
                _instance = this;
            }
        }

        private void Awake() {
            _inventoryItemPrefap = Resources.Load<GameObject>("Prefaps/UI/Inventory/InventoryItem");
            _consumableSlots = transform.GetChild(1).gameObject;
            InventoryManager = GetComponent<InventoryManager>();

        }
    }

