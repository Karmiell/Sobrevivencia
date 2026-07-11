using System;
using UnityEngine;

public class Pedra : BaseItem
{
[SerializeField]private ItemSO itemSO;

  

    public override void Interact()
    {
       OnItemPickEvent(itemSO);

       InteractionSelector.Instance.RemoveItemFromListUsable(this);
       Destroy(gameObject);
    }
    public override string GetNameInteraction()
    {
        return "Pegar";
    }
}
