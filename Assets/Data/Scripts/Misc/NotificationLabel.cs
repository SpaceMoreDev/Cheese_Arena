using UnityEngine;
using UnityEngine.UI;
using Managers;

public class NotificationLabel : MonoBehaviour
{
    public float FadeTime {set{fadeTime = value;} get{return fadeTime;}}
    private float fadeTime = 1.0f;

    void Start()
    {
        StartCoroutine(UIManager.FadeOut(GetComponent<Graphic>(), fadeTime));
    }
}
