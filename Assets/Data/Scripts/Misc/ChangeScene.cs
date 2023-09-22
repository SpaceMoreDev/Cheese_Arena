using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    GameObject Player;
    [SerializeField] LayerMask layer;
    void Start()
    {
        Player = PlayerMovement.current.gameObject;
    }

    private void Update() {
        Collider[] collidedObjs = Physics.OverlapBox(transform.position, transform.localScale, transform.rotation,layer);

        foreach(Collider col in collidedObjs)
        {
            LoadingScene.ChangeScene(1);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
