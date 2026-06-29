using System;
using UnityEngine;

public class Pedra : BaseItem
{
[SerializeField]private ItemSO itemSO;

  

    public override void Interact()
    {
       if(!itemSO.isUsable)return;
       OnItemPickEvent(itemSO);

       InteractionSelector.Instance.RemoveItemFromListUsable(this);
       Destroy(gameObject);
    }
    public override string GetNameInteraction()
    {
        return "Pegar";
    }
}
