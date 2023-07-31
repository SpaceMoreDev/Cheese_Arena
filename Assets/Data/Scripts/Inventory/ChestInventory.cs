using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using Behaviours;

public class ChestInventory : MonoBehaviour
{
    [SerializeField] internal GameObject InventorySlots;
    [SerializeField] internal GameObject PlayerSlots;
    internal int maxItems = 10;
    internal List<GameObject> ObjectList = new();

    Inventory playerInventory;

    internal void RemoveItemsToMenu()
    {
        foreach(GameObject i in ObjectList){
            Destroy(i);
        }
    }

    internal void UpdateMenuItems(Inventory inventory)
    {
        RemoveItemsToMenu();
        playerInventory = PlayerInventoryHandler.Instance.PlayerInventory.Inventory;
        
        foreach(var i in playerInventory.InventoryItems){
            GameObject spawnedItem = Instantiate<GameObject>(PlayerInventoryHandler.Instance.InventoryItemPrefap, PlayerSlots.transform);
            spawnedItem.GetComponent<Image>().sprite = i.Data.Sprite;
            Debug.Log($"- {i.Data.ItemName}");
            ObjectList.Add(spawnedItem);
        }
        foreach(var i in inventory.InventoryItems){
            GameObject spawnedItem = Instantiate<GameObject>(PlayerInventoryHandler.Instance.InventoryItemPrefap, InventorySlots.transform);
            spawnedItem.GetComponent<Image>().sprite = i.Data.Sprite;
            ObjectList.Add(spawnedItem);
        }
    }
    private void Awake() {
        
    }
}
