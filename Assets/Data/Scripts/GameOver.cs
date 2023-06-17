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
        anim.Play("Idle");
        current.deathSound.Stop();
        MainMenu.playing = true;

        TP_PlayerController.current.gameObject.transform.position = PlayerRespawnManager.GetRespawn().position;
        TP_PlayerController.current.gameObject.transform.rotation = PlayerRespawnManager.GetRespawn().rotation;
        TP_PlayerController.current.animator.Play("Sitting");
        TP_PlayerController.current.healthbar.healthBar.fillAmount = 1;
        TP_PlayerController.current.staminabar.staminaBar.fillAmount = 1;
        EnemyRespawnManager.Respawn();
        Invoke("stand",3f);
    }
    void stand()
    {
        TP_PlayerController.current.animator.SetTrigger("Respawned");
        TP_PlayerController.current.alive = true;
    }
}
