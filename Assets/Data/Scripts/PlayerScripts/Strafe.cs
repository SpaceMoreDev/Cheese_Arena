using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using MyBox;

public class Strafe : MonoBehaviour
{
    private Animator m_anim;
    void Start()
    {
        m_anim = GetComponent<Animator>();
        InputManager.inputActions.General.Jump.started += _ => StrafeAction();
    }
    void StrafeAction()
    { 
        m_anim.SetTrigger("Strafe");  
    }
}
