using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnManager : MonoBehaviour
{
    [SerializeField] Transform enemiesMapParent;
    static List<Vector3> CurrentEnemies = new List<Vector3>();
    static GameObject _enemyPrefap;
    private static EnemyRespawnManager _current;
    void Awake()
    {
        _current = this;
        foreach(Enemy enemyObj in enemiesMapParent.GetComponentsInChildren<Enemy>())
        {
            CurrentEnemies.Add(enemyObj.transform.position);
        }

        _enemyPrefap = Resources.Load("Prefaps/Enemies/Enemy") as GameObject;
    }
    public static void Respawn()
    {
        foreach(Enemy enemyObj in _current.enemiesMapParent.GetComponentsInChildren<Enemy>())
        {
            Destroy(enemyObj.gameObject);
        }
        foreach(var enemy in CurrentEnemies)
        {
            Instantiate(_enemyPrefap,enemy,Quaternion.identity,_current.enemiesMapParent);
        }
    }
}
