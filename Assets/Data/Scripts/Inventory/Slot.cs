using UnityEngine;
using UnityEngine.UI;
using Behaviours;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using System.Linq;

public class Slot
{

    internal int _quantityLabel{
        get{
            if(MenuObject != null ){ 
                return int.Parse( MenuObject.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text );
            }
            return -1;
        }set {
            if(MenuObject != null ){
                if(value <= 1)
                {
                    MenuObject.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "";
                }
                else
                {
                    MenuObject.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = value.ToString();
                }
            }
        }
    }
    internal int _quantity = 1;

    public int Number;
    public Item Item;
    public Transform parentPanel;
    public GameObject MenuObject;
    public bool isConsumable;
    

    // internal GameObject _prefap{
    //     // the prefap location to be able to spawn the UI items.
    //     get => Resources.Load<GameObject>("Prefaps/UI/Inventory/InventoryItem");
    // }

    public Slot(int num, Transform parent, Item item)
    {
        this.Number = num;
        this.Item = item;
        this.parentPanel = parent;
        this.isConsumable = false;
        this.MenuObject = GameObject.Instantiate(item.currentInventory.SlotPrefap,parent);
        this.MenuObject.GetComponent<Image>().color = Color.white;
        UpdateSlot();
    }
    public Slot()
    {
    }
    public Slot(int num, Transform parent, Item item, bool consumable)
    {
        this.Number = num;
        this.Item = item;
        this.parentPanel = parent;
        this.isConsumable = consumable;
        this.MenuObject = GameObject.Instantiate(item.currentInventory.SlotPrefap,parent);
        this.MenuObject.GetComponent<Image>().color = Color.white;
        UpdateSlot();
        
    }

    public void AddToQuantity()
    {
        _quantity ++;
        _quantityLabel = _quantity;
    }

    public void RemoveFromQuantity()
    {
        _quantity --;
        _quantityLabel = _quantity;
    }

    internal void UpdateSlot()
    {
        if(this.isConsumable){      
            this.MenuObject.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = this.Number.ToString();
        }
        if(Item.Data != null){
            this.MenuObject.GetComponent<Image>().sprite = Item.Data.Sprite;
            this.MenuObject.GetComponent<Image>().color = Color.white;
        }
        else{
            this.MenuObject.GetComponent<Image>().color = Color.gray;
        }
        this.MenuObject.GetComponent<DragItems>().DragData = this;
        this._quantityLabel = this._quantity;
    }

    /// <summary>
    /// Use Item in consumable slots based on player's input.
    /// </summary>
    /// <param name="ctx"> Action context </param>
    public void ConsumeItem()
    {
        if(this.Item.Data != null)
        {
            Debug.Log($"used item ID{this.Item.Data.ID} - {this.Item.Data.ItemName} - Quantity {this._quantity}");
            this.RemoveFromQuantity();
            // do stuff
            this.Item.Use(PlayerInventory.Player.gameObject);   
        }
        else{
            Debug.Log("empty slot!");
        }    
    }

    /// <summary>
    /// This function checks if the items are the same to stack them together.
    /// </summary>
    /// <returns></returns>
    bool CheckStack(Slot slot)
    {
        // if both have the same itemObject.
        if(this.Item.Data == slot.Item.Data)
        {
            //Adding to the quantity variable.
            this.AddToQuantity();
            //Removing the dragged item.
            slot.Item.currentInventory.RemoveFromInventory(slot);

            Debug.Log("Stack!");
            return true;
        }
        return false;
    }

    /// <summary>
    /// This functions is what happens when you drag another slot on it.
    /// </summary>
    /// <param name="dragged"></param>
    public void DragOn(ref Slot dragged)
    {
        if(!CheckStack(dragged)){
            if(this.isConsumable){
                if(!dragged.isConsumable){
                    ConsumeItems.current.AddItemToConsume(dragged, this);
                }
                {
                    Slot dub = (Slot)this.Clone();
                    this.ReplaceWithSlot(dragged);
                    dragged.ReplaceWithSlot(dub);
                    ConsumeItems.current.Inventory.UpdateMenuItems();
                }
            }
            else {
                if(dragged.isConsumable){
                    ConsumeItems.current.RemoveItemToConsume(dragged, this.Item.currentInventory);
                    ConsumeItems.current.Inventory.UpdateMenuItems();
                }

                (dragged.Number, this.Number) = (this.Number, dragged.Number);
                Slot dub = (Slot)this.Clone();
                this.ReplaceWithSlot(dragged);
                dragged.ReplaceWithSlot(dub);
            }
        } 
    }

    /// <summary>
    /// Replaces current slot with an empty slot.
    /// </summary>
    /// <param name="consumable">Is it a consumable slot?</param>
    /// <returns></returns>
    public void ReplaceWithEmpty(bool consumable)
    {   Item emptyItem = new Item(this.Item.currentInventory);
         
        GameObject.Destroy(MenuObject);
        MenuObject = GameObject.Instantiate(emptyItem.currentInventory.SlotPrefap, parentPanel);
        
        this._quantityLabel = 1;
        this.Item = emptyItem;
        emptyItem.currentInventory.UpdateMenuItems();
    } 
    public object Clone()
    {
        return this.MemberwiseClone();
        // to use: object temp = (object)this.Clone();
    }

    public void ReplaceWithSlot(Slot slot)
    {
        // this.Item.currentInventory.TransferItem(this,slot.Item.currentInventory);
        this.Item = slot.Item;
        this._quantity = slot._quantity;
        if(!this.isConsumable && !slot.isConsumable){ this.Number = slot.Number; }
        this.Item.currentInventory.UpdateMenuItems();

    } 
}