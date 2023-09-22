using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject LoadingPanel;
    [SerializeField] private Image LoadingBar;
    AsyncOperation asyncOperation;
    static LoadingScene current;
    
    void Awake()
    {
        current = this;
        // DontDestroyOnLoad(gameObject);
    }

    public static void ChangeScene(int index)
    {
        current.StartCoroutine(current.LoadScene_Coroutine(index));
    }
    
    IEnumerator LoadScene_Coroutine(int index)
    {
        LoadingBar.fillAmount = 0;
        asyncOperation = SceneManager.LoadSceneAsync(index,LoadSceneMode.Single);
        asyncOperation.allowSceneActivation = false;
        LoadingPanel.SetActive(true);
        float progress = 0;

        while(!asyncOperation.isDone){
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            LoadingBar.fillAmount = progress;
            if (progress >= 0.9f)
            {
                LoadingBar.fillAmount = 1;
                asyncOperation.allowSceneActivation = true;
                Debug.Log("Pro :" + progress);
            }
            
            yield return null;
        }
        LoadingPanel.SetActive(false);
    }
}
