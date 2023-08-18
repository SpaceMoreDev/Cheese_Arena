using UnityEngine;
using Behaviours;
using Managers;
using UnityEngine.InputSystem;

public class ConsumeItems : MonoBehaviour
{
    [SerializeField] public int MaxConsumeItems = 2;
    private static GameObject _consumablePrefap{
        get => Resources.Load<GameObject>("Prefaps/UI/Inventory/ConItem");
    }
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
        Inventory.SlotPrefap = _consumablePrefap;

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
        foreach(Slot i in Inventory.ItemSlots){

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
        toAdd.isConsumable = true;
        alreadyThere.ReplaceWithSlot(toAdd);
        Inventory.UpdateMenuItems();
    }
    /// <summary>
    /// Remove an Item from the consume slots.
    /// </summary>
    /// <param name="toRemove">what to remove?</param>
    public Slot RemoveItemToConsume(Slot toRemove, Inventory newInventory)
    {
        Item item = new(Inventory);
        Slot con_slot = new( // create an empty slot
            toRemove.Number,
            _consumeParentPanel.transform,
            item,
            true
        );
        Inventory.AddToInventory(con_slot); //adding the empty slot
        toRemove.isConsumable = false;
        Inventory.RemoveFromInventory(toRemove);
        newInventory.AddToInventory(toRemove);

        return toRemove;
    }

    public Slot RemoveItem(Slot toRemove)
    {
        Item item = new(Inventory);
        Slot con_slot = new( // create an empty slot
            toRemove.Number,
            _consumeParentPanel.transform,
            item,
            true
        );
        Inventory.AddToInventory(con_slot); //adding the empty slot
        toRemove.isConsumable = false;
        Inventory.RemoveFromInventory(toRemove);

        return toRemove;
    }
}
