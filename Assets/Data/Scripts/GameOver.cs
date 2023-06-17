using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] bool restart = false;
    [SerializeField] AudioSource deathSound;
    [SerializeField] bool playSound= false; 
    public static GameOver current;
    public UnityEvent PlayMusic;
    public Animator anim;

    void Awake()
    {
        current = this;
        anim = GetComponent<Animator>();
    }

    public void PlayDeath()
    {
        current.deathSound.Play();
    }

    public void RespawnPlayer()
    {
        TP_PlayerController.current.alive = true;
        TP_PlayerController.current.gameObject.transform.position = PlayerRespawnManager.GetRespawn();
        TP_PlayerController.current.animator.Play("Sitting");
        TP_PlayerController.current.animator.SetTrigger("Respawned");
        TP_PlayerController.current.healthbar.healthBar.fillAmount = 1;
        TP_PlayerController.current.staminabar.staminaBar.fillAmount = 1;
        MainMenu.playing = true;
        current.deathSound.Stop();
        anim.Play("Idle");
    }
}
