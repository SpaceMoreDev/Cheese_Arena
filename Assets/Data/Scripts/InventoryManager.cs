using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private GameObject _inventoryItemPrefap;
    [SerializeField] private GameObject _inventorySlots;
    [SerializeField] private GameObject _inventoryMenu;

    private GameObject _consumableSlots;
    public int maxItems = 10;
    private static InventoryManager _instance;
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

    public static InventoryManager Instance {
        get{
            if(_instance == null){
                return null;
            }
            return _instance;
        }
    }
    
    public InventoryManager()
    {
        if(_instance == null){
            _instance = this;
        }
    }

    internal void AddItemsToMenu(List<ItemObject> items)
    {
        foreach(var i in items){
            Item item = new(i);
            GameObject spawnedItem = Instantiate(InventoryItemPrefap, Vector3.zero, Quaternion.identity, InventorySlots.transform);
            spawnedItem.GetComponent<Image>().sprite = item.Data.Sprite;
            ObjectList.Add(spawnedItem);
        }
        PlayerCameraHandler.Instance.SetCamerPOV(false);
    }

    internal void RemoveItemsToMenu()
    {
        foreach(GameObject i in ObjectList){
            MonoBehaviour.Destroy(i);
        }
        PlayerCameraHandler.Instance.SetCamerPOV(true);
    }

    internal void GetItemsToMenu(Inventory inventory)
    {
        
    }

    private void Awake() {
        _inventoryItemPrefap = Resources.Load<GameObject>("Prefaps/UI/Inventory/InventoryItem");
        _consumableSlots = _inventoryCanvas.transform.GetChild(1).gameObject;

    }

}
