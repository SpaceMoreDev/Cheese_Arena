using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

public class ChestInventory : MonoBehaviour
{
    [SerializeField] internal GameObject InventorySlots;
    internal int maxItems = 10;
    internal List<GameObject> ObjectList = new();

    internal void RemoveItemsToMenu()
    {
        foreach(GameObject i in ObjectList){
            Destroy(i);
        }
    }

    internal void UpdateMenuItems(Inventory inventory)
    {
        RemoveItemsToMenu();
        foreach(var i in inventory.InventoryItems){
            GameObject spawnedItem = Instantiate<GameObject>(PlayerInventoryManager.Instance.InventoryItemPrefap, InventorySlots.transform);
            spawnedItem.GetComponent<Image>().sprite = i.Data.Sprite;
            ObjectList.Add(spawnedItem);
            Debug.Log("chest = added to UI");
        }
    }
}
