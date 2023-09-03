using System.Collections;
using System;
using UnityEngine;
using Managers;

[RequireComponent(typeof(HealthManager), typeof(RandomItemSpawner))]
public class Destructable : MonoBehaviour
{
    [HideInInspector] public HealthManager health;
    RandomItemSpawner itemSpawner;
    [SerializeField] bool canSpawn = true;
    [SerializeField] float destroyDelay = 0.2f;
    public Action<Destructable> destroyedItemAction;

    void Awake()
    {
        health = GetComponent<HealthManager>();
        itemSpawner = GetComponent<RandomItemSpawner>();

        health.healthbar.healthBarEmpty += this.DestroyObject;
        health.healthbar.healthBarDecrease += this.DamageAnim;
    }

    void OnDestroy()
    {
        health.healthbar.healthBarEmpty -= this.DestroyObject;
        health.healthbar.healthBarDecrease -= this.DamageAnim;

    }

    void DamageAnim()
    {    
        if(this.TryGetComponent<Animator>(out Animator anim))
        {
            anim.Play("Damage");
            Debug.Log("Called..");
        }
        
    }

    void DestroyObject(GameObject destroyedObject)
    {
        if(canSpawn)
        {
            GameObject pickup =  itemSpawner.Spawn();
            pickup.transform.position += new Vector3(0,0.3f,0);
        }

        destroyedItemAction?.Invoke(this);
        Destroy(this.gameObject,destroyDelay);
        // Debug.Log("Its working!");

    }
}
