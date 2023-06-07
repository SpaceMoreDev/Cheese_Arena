using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionUI : MonoBehaviour
{
    [SerializeField] GameObject selectionUI;

    public void SetObjectVisibility(bool isVisible)
    {
        selectionUI.SetActive(isVisible);
    }
}
