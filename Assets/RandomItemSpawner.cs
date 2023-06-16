using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class RandomItemSpawner : MonoBehaviour
{
    List<GameObject> items;
    
    void Start()
    {
        items = ItemPickupsManager.current.pickupObjects;
    }

    public GameObject Spawn()
    {
        int randID = Random.Range(0, items.Count);
        GameObject spawnedObj = Instantiate(items[randID], transform.position , Quaternion.identity);
        return spawnedObj;
    }
    public GameObject Spawn(int itemID)
    {
        if(itemID < items.Count)
        {
            GameObject spawnedObj = Instantiate(items[itemID], transform.position , Quaternion.identity);
            return spawnedObj;
        }
        else
        {
            Debug.LogError($"> Item doesnt exist in the pickup list. Item: {itemID}");  
        }
        return null;
    }
}
