using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

[RequireComponent(typeof(HealthManager), typeof(RandomItemSpawner))]
public class Destructable : MonoBehaviour
{
    [HideInInspector] public HealthManager health;
    RandomItemSpawner itemSpawner;
    [SerializeField] float destroyDelay = 0.2f;

    void Awake()
    {
        health = GetComponent<HealthManager>();
        itemSpawner = GetComponent<RandomItemSpawner>();

        health.healthbar.healthBarEmpty += this.DestroyObject;
    }
    void OnDestroy()
    {
        health.healthbar.healthBarEmpty -= this.DestroyObject;
    }

    void DestroyObject(GameObject destroyedObject)
    {

        GameObject pickup =  itemSpawner.Spawn();
        pickup.transform.position += new Vector3(0,0.3f,0);
        Destroy(this.gameObject,destroyDelay);
        Debug.Log("Its working!");

    }
}
