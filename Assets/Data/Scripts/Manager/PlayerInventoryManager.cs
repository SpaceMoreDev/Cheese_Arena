using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Managers{
    
    public class PlayerInventoryManager : MonoBehaviour //todo remove this
    {
        [SerializeField] private Canvas _inventoryCanvas;
        [SerializeField] private GameObject _inventoryItemPrefap;
        [SerializeField] private GameObject _inventorySlots;
        [SerializeField] private GameObject _inventoryMenu;

        private GameObject _consumableSlots;
        public int maxItems = 10;
        private static PlayerInventoryManager _instance;
        public List<GameObject> ObjectList;

        [HideInInspector] public GameObject InventorySlots { 
            get => _inventorySlots; 
            set => _inventorySlots = value;
        }

        [HideInInspector] public GameObject InventoryMenu { 
            get => _inventoryMenu; 
        }

        [HideInInspector] public GameObject ConsumableSlots { 
            get => _consumableSlots; 
            set => _consumableSlots = value;
        }

        [HideInInspector] public Canvas InventoryCanvas {
            get=> _inventoryCanvas; 
            set=> _inventoryCanvas = value;
        }

        [HideInInspector] public GameObject InventoryItemPrefap{
            get=> _inventoryItemPrefap; 
            set=> _inventoryItemPrefap = value;
        }

        public static PlayerInventoryManager Instance {
            get{
                return _instance;
            }
        }
        
        public PlayerInventoryManager()
        {
            if(_instance == null){
                _instance = this;
            }
        }

        internal void RemoveItemsToMenu()
        {
            foreach(GameObject i in ObjectList){
                MonoBehaviour.Destroy(i);
            }
        }

        internal void UpdateMenuItems(Inventory inventory)
        {
            RemoveItemsToMenu();
            foreach(Item i in inventory.InventoryItems){
                GameObject spawnedItem = Instantiate(InventoryItemPrefap, Vector3.zero, Quaternion.identity, InventorySlots.transform);
                spawnedItem.GetComponent<Image>().sprite = i.Data.Sprite;
                ObjectList.Add(spawnedItem);
                Debug.Log("added to UI");
            }
        }

        private void Awake() {
            _inventoryItemPrefap = Resources.Load<GameObject>("Prefaps/UI/Inventory/InventoryItem");
            _consumableSlots = _inventoryCanvas.transform.GetChild(1).gameObject;

        }
    }
}
