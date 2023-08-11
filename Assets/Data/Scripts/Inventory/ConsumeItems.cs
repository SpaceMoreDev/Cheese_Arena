using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Behaviours;
using Managers;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Interactions;

public class ConsumeSlot
{   
    private int _quantity = 1;

    public int Number = 0;
    public GameObject UIobject;
    public Item Item;
    public Inventory Inventory;
     public static GameObject _emptySlotPrefap{
        get => Resources.Load<GameObject>("Prefaps/UI/Inventory/EmptyConsumption");
    }
    public int Quantity {
        get => this._quantity;
        set => this._quantity = value;
    }

    public ConsumeSlot(int num, GameObject gameobject, Item item)
    {
        this.Number = num;
        this.UIobject = gameobject;
        this.Item = item;
    }

    public void AddToQuantity()
    {
        this._quantity ++;
        this.UIobject.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{this._quantity}";
    }

    public void RemoveFromQuantity()
    {
        this._quantity --;
        this.UIobject.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{this._quantity}";
    }

    public ConsumeSlot ReplaceWithEmpty()
    {
        ConsumeSlot EmptySlot = new ConsumeSlot(
            Number,
            UIobject,
            null
        );
        EmptySlot.UIobject.GetComponent<Image>().color = Color.gray;
        EmptySlot.UIobject.GetComponent<Image>().sprite = _emptySlotPrefap.GetComponent<Image>().sprite;
        EmptySlot.UIobject.GetComponent<ConSlotDrag>().consumeSlot = EmptySlot;
        EmptySlot.UIobject.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
        EmptySlot.Inventory = Inventory;
        EmptySlot.Quantity = 0;

        return EmptySlot;
    } 

    public ConsumeSlot ReplaceWithSlot(ConsumeSlot Slot)
    {
        Slot.UIobject.GetComponent<Image>().color = Color.gray;
        Slot.UIobject.GetComponent<Image>().sprite = Slot.UIobject.GetComponent<Image>().sprite;
        Slot.UIobject.GetComponent<ConSlotDrag>().consumeSlot = Slot;
        Slot.UIobject.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
        Slot.Inventory = Inventory;
        Slot.Quantity = 1;

        return Slot;
    } 
}

public class ConsumeItems : MonoBehaviour
{
    internal static List<ConsumeSlot> _consumeSlots;
    [SerializeField] public int MaxConsumeItems = 2;
    
    private Inventory _inventory;
    public  Inventory Inventory {
        get{
            if(_inventory == null){
                _inventory = new Inventory();
            }
            return _inventory;
        }
    }

    private Transform _consumeParentPanel;
    public static Transform ConsumePanel;

   
    private void Awake() {
        InputManager.inputActions.General.Consume.started += ConsumeItem;
    }
    
    private void Start() {
        _consumeParentPanel = transform;
        ConsumePanel = _consumeParentPanel;
        Inventory.Panel = ConsumePanel.gameObject;
        _consumeSlots = new List<ConsumeSlot>();
        UpdateSlots();
    }

    /// <summary>
    /// Use Item in consumable slots based on player's input.
    /// </summary>
    /// <param name="ctx"> Action context </param>
    void ConsumeItem(InputAction.CallbackContext ctx)
    {
        foreach(ConsumeSlot i in _consumeSlots){

            if(ctx.control.displayName == (i.Number).ToString()){
                if(i.Item != null)
                {
                    Debug.Log($"used item ID{i.Item.Data.ID} - {i.Item.Data.ItemName} - Quantity {i.Quantity}");
                    i.RemoveFromQuantity();

                    // do stuff
                    i.Item.Use(PlayerInventory.Player.gameObject);
                    Inventory.InventoryItems.Remove(i.Item);
                    
                    if(i.Quantity <= 0){
                        ConsumeSlot a = i;
                        ConsumeSlot b = i.ReplaceWithEmpty();
                        _consumeSlots.Add(b);
                        ReplaceSlot(ref a,ref b);
                    }

                    break;
                }
                else{
                    Debug.Log("empty slot!");
                }        
            }
        }
    }

    /// <summary>
    /// Sorts the slots in an ascending order.
    /// </summary>
    public static void Sort(List<ConsumeSlot> slots)
    {
        slots.OrderBy(x => x.Number);
        foreach(ConsumeSlot i in _consumeSlots){
            if(i.Item != null)
            {
                Debug.Log($"---  {i.Item.Data.ItemName} => {i.Number}.");
            }
            i.UIobject.transform.GetChild(1).GetComponent<TMP_Text>().text = i.Number.ToString(); 
        }
    }

    void UpdateSlots()
    {
        foreach( ConsumeSlot i in _consumeSlots){
            Destroy(i.UIobject);
        }

        _consumeSlots.Clear();
        
        int ct = 1;
        int max = 0;


        // foreach(Item i in inventory.InventoryItems)
        // {
        //     if(max < PlayerInventory.Player.MaxItems)
        //     { 
        //         ConsumeSlot con_slot = _consumeSlots.Find(x => x.Item.Data.ID == i.Data.ID);
                
        //         if(con_slot != null){
        //             con_slot.AddToQuantity();
        //             continue;
        //         }else{ 
        //             GameObject slot = Instantiate(_emptySlotPrefap,_consumeParentPanel.transform);
        //             con_slot = new ConsumeSlot(
        //                 ct,
        //                 slot,
        //                 i
        //             );

        //             ct++;
        //         }
        //         con_slot.UIobject.GetComponent<Image>().sprite = i.Data.Sprite;
        //         con_slot.UIobject.GetComponent<Image>().color = Color.white;
        //         con_slot.UIobject.GetComponent<DragItems>().itemData = i;
        //         con_slot.UIobject.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{con_slot.Quantity}";
        //         _consumeSlots.Add(con_slot);
        //         max++;
        //     }

        // }
        
        
        // Debug.Log($"the max items is {PlayerInventory.Player.MaxItems}.");
        while(max < MaxConsumeItems)
        {
            GameObject slot = Instantiate(ConsumeSlot._emptySlotPrefap,_consumeParentPanel.transform);
            ConsumeSlot con_slot = new ConsumeSlot(
                        ct,
                        slot,
                        null
                    );
            // con_slot.UIobject.GetComponent<DragItems>().itemData = null;
            con_slot.UIobject.GetComponent<Image>().color = Color.gray;
            con_slot.UIobject.GetComponent<ConSlotDrag>().consumeSlot = con_slot;
            con_slot.Inventory = Inventory;
            _consumeSlots.Add(con_slot);
            ct++;
            max++;
        }

        Sort(_consumeSlots);
        Inventory.UpdateMenuItems();
    }

    /// <summary>
    /// Replaces the consume Slot with another Item
    /// </summary>
    public static void ReplaceSlot(ref ConsumeSlot a, ref ConsumeSlot b)
    {
        if(b.Item != null){
            a.UIobject.GetComponent<Image>().sprite = b.Item.Data.Sprite;
            a.UIobject.GetComponent<Image>().color = Color.white;
        }
        else{
            a.UIobject.GetComponent<Image>().sprite = ConsumeSlot._emptySlotPrefap.GetComponent<Image>().sprite;
            a.UIobject.GetComponent<Image>().color = Color.gray;
        }
        
        a.UIobject.GetComponent<ConSlotDrag>().consumeSlot =b;
        a.Inventory = b.Inventory;
        a.Number = b.Number;
        a.Item = b.Item;

        if(b.Quantity > 0)
            a.UIobject.transform.GetChild(0).GetComponent<TMP_Text>().text = b.Quantity.ToString();
        else
            a.UIobject.transform.GetChild(0).GetComponent<TMP_Text>().text = "";

    }

    // void IDropHandler.OnDrop(PointerEventData eventData)
    // {
    //     Debug.Log("dropped");

    //     if(eventData.pointerDrag != null)
    //     {
    //         int ct = 0;
    //         foreach(ConsumeSlot i in _consumeSlots){
    //             if(i.Item != null)
    //             {
    //                 ct++;
    //             }
    //         }


    //         if(ct < PlayerInventory.Player.MaxItems)
    //         {
    //             Item item = eventData.pointerDrag.GetComponent<DragItems>().itemData;
    //             Inventory currentInventory = item.currentInventory;

    //             GameObject slot = Instantiate(_emptySlotPrefap,_consumeParentPanel.transform);

                


    //             ConsumeSlot tobereplaced = null;
    //             foreach(ConsumeSlot i in _consumeSlots)
    //             {
    //                 if(i.Item == null)
    //                 {
    //                     tobereplaced = i;
    //                     break;
    //                 }
    //             }

    //             if(tobereplaced != null)
    //             {
    //                 ConsumeSlot con_slot = new ConsumeSlot(
    //                     tobereplaced.Number,
    //                     slot,
    //                     item
    //                 );
                    
    //                 Inventory.TransferItem(item, currentInventory, Inventory);
    //                 con_slot.UIobject.GetComponent<Image>().sprite = item.Data.Sprite;
    //                 con_slot.UIobject.GetComponent<Image>().color = Color.white;
    //                 con_slot.UIobject.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
    //                 con_slot.UIobject.transform.SetParent(tobereplaced.UIobject.transform.parent);

    //                 _consumeSlots.Add(con_slot);
    //                 _consumeSlots.Remove(tobereplaced);
                    
    //                 Destroy(eventData.pointerDrag);
    //                 Destroy(tobereplaced.UIobject);

    //                 Sort(_consumeSlots);

    //                 Debug.Log($"moved {item.Data.ItemName} into consume inventory");
    //             }
                
    //         }

    //     }
    // }
}
