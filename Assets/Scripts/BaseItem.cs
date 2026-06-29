using System;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{  
    public static event  EventHandler<OnItemPickEventArgs> OnItemPick;
    public class OnItemPickEventArgs : EventArgs
    {
    public ItemSO itemSO; 
    }


    public virtual void Interact(){}

    public virtual string GetNameInteraction() => "";

    protected void OnItemPickEvent(ItemSO itemSO)
    {
        OnItemPick?.Invoke(this, new OnItemPickEventArgs(){itemSO = itemSO});
    }
   
}
