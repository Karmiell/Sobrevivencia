using System;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{  
    public static event  EventHandler<OnItemPickEventArgs> OnItemPick;
    public static event  EventHandler<OnItemPickEventArgs> OnItemDrop;
    public class OnItemPickEventArgs : EventArgs
    {
    public ItemSO itemSO; 
    }



    public virtual void PickUp(){}
    public virtual void Drop(Transform player){}

    public virtual string GetNameInteraction() => "";

    protected void OnItemPickEvent(ItemSO itemSO)
    {
        OnItemPick?.Invoke(this, new OnItemPickEventArgs(){itemSO = itemSO});
    }
    protected void OnItemDropEventCall(ItemSO itemSO)
    {
        OnItemDrop?.Invoke(this, new OnItemPickEventArgs(){itemSO = itemSO});
    }
   
}
