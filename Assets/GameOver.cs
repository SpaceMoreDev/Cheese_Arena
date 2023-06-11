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

    void Update()
    {

        if(restart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
