using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Behaviours;
using Managers;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

class ConsumeSlot
{   
    private int _quantity = 1;

    public int Number = 0;
    public GameObject UIobject;
    public Item Item;
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
}

public class ConsumeItems : MonoBehaviour
{
    private List<ConsumeSlot> _consumeSlots;
    private Inventory inventory;
    private Transform _consumeParentPanel;

    private GameObject _emptySlotPrefap{
        get => Resources.Load<GameObject>("Prefaps/UI/Inventory/EmptyConsumption");
    }
    private void Awake() {
        InputManager.inputActions.General.Consume.started += ConsumeItem;
    }
    
    private void Start() {
        _consumeParentPanel = transform;
        inventory = PlayerInventory.Player.Inventory;
        _consumeSlots = new List<ConsumeSlot>();
        UpdateSlots();
    }

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
                    inventory.InventoryItems.Remove(i.Item);
                    
                    ConsumeSlot usedSlot = i;
                    if(i.Quantity <= 0){

                        ConsumeSlot empty = new ConsumeSlot(
                            i.Number,
                            i.UIobject,
                            null
                        );

                        empty.UIobject.GetComponent<Image>().sprite = _emptySlotPrefap.GetComponent<Image>().sprite;
                        empty.UIobject.GetComponent<Image>().color = Color.gray;
                        empty.Quantity = 0;
                        empty.UIobject.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
                        
                        ReplaceSlot(ref usedSlot,ref empty);
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
    void Sort(List<ConsumeSlot> slots)
    {
        slots.OrderBy(x => x.Number);
        foreach(ConsumeSlot i in _consumeSlots){
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


        foreach(Item i in inventory.InventoryItems)
        {
            if(max < PlayerInventory.Player.MaxItems)
            { 
                ConsumeSlot con_slot = _consumeSlots.Find(x => x.Item.Data.ID == i.Data.ID);
                
                if(con_slot != null){
                    con_slot.AddToQuantity();
                    continue;
                }else{ 
                    GameObject slot = Instantiate(_emptySlotPrefap,_consumeParentPanel.transform);
                    con_slot = new ConsumeSlot(
                        ct,
                        slot,
                        i
                    );

                    ct++;
                }
                con_slot.UIobject.GetComponent<Image>().sprite = i.Data.Sprite;
                con_slot.UIobject.GetComponent<Image>().color = Color.white;
                con_slot.UIobject.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{con_slot.Quantity}";
                _consumeSlots.Add(con_slot);
                max++;
            }

        }
        
        
        // Debug.Log($"the max items is {PlayerInventory.Player.MaxItems}.");
        while(max < PlayerInventory.Player.MaxItems)
        {
            GameObject slot = Instantiate(_emptySlotPrefap,_consumeParentPanel.transform);
            ConsumeSlot con_slot = new ConsumeSlot(
                        ct,
                        slot,
                        null
                    );
            con_slot.UIobject.GetComponent<Image>().color = Color.gray;
            _consumeSlots.Add(con_slot);
            ct++;
            max++;
        }

        Sort(_consumeSlots);
        inventory.UpdateMenuItems(PlayerInventory.Player._inventorySlots);
    }

    /// <summary>
    /// Replaces the consume Slot with another Item
    /// </summary>
    void ReplaceSlot(ref ConsumeSlot a, ref ConsumeSlot b)
    {
        // ConsumeSlot temp = a;
        // a = b;
        // b = temp;
        if(b.Item != null){
            a.UIobject.GetComponent<Image>().sprite = b.Item.Data.Sprite;
            a.UIobject.GetComponent<Image>().color = Color.white;

        }
        else{
            a.UIobject.GetComponent<Image>().sprite = _emptySlotPrefap.GetComponent<Image>().sprite;
            a.UIobject.GetComponent<Image>().color = Color.gray;
        }

        a.Item = b.Item;
        if(b.Quantity > 0)
            a.UIobject.transform.GetChild(0).GetComponent<TMP_Text>().text = b.Quantity.ToString();
        else
            a.UIobject.transform.GetChild(0).GetComponent<TMP_Text>().text = "";

    }
}
