using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using MyBox;

[CreateAssetMenu(fileName = "New Item", menuName = "Create Item")]
public class ItemObject : ScriptableObject
{
    [HideInInspector]
    public static List<ItemObject> Allitems = new List<ItemObject>();
    [ReadOnly] public int ID;
    public string ItemName;
    public float EffectValue;
    public Sprite Sprite;
    public string Description;
    public EffectTypes Effect;
    public Color HighlightColor = Color.white;
    public bool pickable = false;
    [Separator]
    [ConditionalField("pickable")]  public bool SpawnOnDeath = false;
    [ConditionalField("pickable")]  public GameObject prefap = null; // only if pickable
    
    #if UNITY_EDITOR // conditional compilation is not mandatory
    [ButtonMethod]
    private void ResetID()
    {
        // for making sure that no item has the same ID.
        System.Random rnd = new System.Random();
        ID = rnd.Next();
        for(int i=0 ; i< Allitems.Count ; i++)
        {
            if(ID == Allitems[i].ID)
            {
                ID = rnd.Next();
                i = 0;
            }
        }
    }
    #endif

    ItemObject()
    {
        // for making sure that no item has the same ID.
        System.Random rnd = new System.Random();
        ID = rnd.Next();
        for(int i=0 ; i< Allitems.Count ; i++)
        {
            if(ID == Allitems[i].ID)
            {
                ID = rnd.Next();
                i = 0;
            }
        }
        Allitems.Add(this);
    }

}