using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobCutsceneManager : MonoBehaviour
{
    [SerializeField] List<Destructable> ropes;
    [SerializeField] Animator Bob;

    void  Awake()
    {
        foreach(var rope in ropes)
        {
            rope.destroyedItemAction += AffectBob;
        }
    }

    void AffectBob(Destructable rope)
    {
        if(ropes.Count>1)
        {
            Bob.SetTrigger("OneHand");
        }
        else
        {
            Bob.SetTrigger("Fall");
        }
        ropes.Remove(rope);
    }
}
