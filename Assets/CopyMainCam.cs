using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMainCam : MonoBehaviour
{
    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;    
    }

    void Update()
    {
        transform.rotation = mainCam.transform.rotation;
    }
}
