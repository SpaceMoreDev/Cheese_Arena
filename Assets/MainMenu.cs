using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using  Cinemachine;
using Managers;

public class MainMenu : MonoBehaviour
{
    public static bool playing = false;
    [SerializeField] PlayableDirector cutscene;
    private PlayableDirector mainMenuPlayable;
    [SerializeField] Button PlayButton;
    [SerializeField] Button HelpButton;
    [SerializeField] Button OptionsButton;
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject HelpPanel;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Toggle isFullScreen;
    public AudioSource musicSource;
    public AudioSource bgmusicSource;

    [SerializeField] public CinemachineVirtualCamera main_camera;
    void Start()
    {
        TP_PlayerController.current.animator.Play("Sitting");
        mainMenuPlayable = GetComponent<PlayableDirector>();
    }
    // Start is called before the first frame update
    public void Play()
    {
        mainMenuPlayable.Stop();
        cutscene.Play();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Options()
    {
        OptionsButton.interactable = false;
        PlayButton.interactable = false;
        HelpButton.interactable = false;
        OptionsPanel.SetActive(true);
    }

    
    public void Help()
    {
        OptionsButton.interactable = false;
        PlayButton.interactable = false;
        HelpButton.interactable = false;
        HelpPanel.SetActive(true);
    }

    public void Back()
    {
        OptionsButton.interactable = true;
        PlayButton.interactable = true;
        HelpButton.interactable = true;
        OptionsPanel.SetActive(false);
        HelpPanel.SetActive(false);
    }
    public void OnToggleValueChanged()
    {
        Screen.fullScreen = isFullScreen.isOn;
    }
    public void OnSliderValueChanged()
    {
        musicSource.volume = volumeSlider.value;
        bgmusicSource.volume = volumeSlider.value-0.2f;
        // Debug.Log($"changed {volumeSlider.value}");
    }
}
