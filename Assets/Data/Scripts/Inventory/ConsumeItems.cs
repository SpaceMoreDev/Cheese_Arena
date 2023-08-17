using UnityEngine;
using Behaviours;
using Managers;
using UnityEngine.InputSystem;

public class ConsumeItems : MonoBehaviour
{
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

    public static ConsumeItems current;

    private void Awake() {
        current = this;
        InputManager.inputActions.General.Consume.started += ConsumeItem;
    }
    
    private void Start() {
        _consumeParentPanel = transform;
        ConsumePanel = _consumeParentPanel;
        Inventory.Panel = ConsumePanel.gameObject;

        int ct = 1;
        int max = 0;
        
        while(max < MaxConsumeItems)
        {   
            if(max >= MaxConsumeItems){break;}
            Item item = new(Inventory);
            Slot con_slot = new(
                        ct,
                        _consumeParentPanel.transform,
                        item,
                        true
                    );
            con_slot.ReplaceWithEmpty(true);
            Inventory.AddToInventory(con_slot);
            ct++;
            max++;
        }
        Inventory.UpdateMenuItems();
    }

    /// <summary>
    /// Use Item in consumable slots based on player's input.
    /// </summary>
    /// <param name="ctx"> Action context </param>
    void ConsumeItem(InputAction.CallbackContext ctx)
    {
        foreach(Slot i in Inventory.itemSlots){

            if(ctx.control.displayName == (i.Number).ToString()){
                i.ConsumeItem();
                if(i._quantity < 1){
                    i.ReplaceWithEmpty(true); 
                    Inventory.UpdateMenuItems();
                }
                break;   
            }
        }
        // Inventory.UpdateMenuItems();
    }
    /// <summary>
    /// This function will replace an existing consumable slot with another.
    /// </summary>
    /// <param name="toAdd">slot to add</param>
    /// <param name="alreadyThere">choose which slot to add in its place</param>
    public void AddItemToConsume(Slot toAdd, Slot alreadyThere)
    {
        toAdd.Item.currentInventory.RemoveFromInventory(toAdd);
        alreadyThere.ReplaceWithSlot(toAdd);
        toAdd.isConsumable = true;
        Inventory.UpdateMenuItems();
    }
    /// <summary>
    /// Remove an Item from the consume slots.
    /// </summary>
    /// <param name="toRemove">what to remove?</param>
    public void RemoveItemToConsume(Slot toRemove)
    {
        Item item = new(Inventory);
        Slot con_slot = new(
            toRemove.Number,
            _consumeParentPanel.transform,
            item,
            true
        );
        AddItemToConsume(con_slot, toRemove);
        Inventory.RemoveFromInventory(toRemove);

        toRemove.isConsumable = false;
        Inventory.UpdateMenuItems();
    }
}
