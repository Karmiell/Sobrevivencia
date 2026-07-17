using System;
using Unity.Mathematics;
using UnityEngine;

public class PickUp_Drop : BaseItem
{
[SerializeField]private ItemSO itemSO;

  

    public override void PickUp()
    {
       OnItemPickEvent(itemSO);

       InteractionSelector.Instance.RemoveItemFromListUsable(this);
       Destroy(gameObject);
    }
    public override void Drop(Transform player)
    {
       var dropPosition = player.transform.position + player.transform.forward;
       Instantiate(itemSO.visual, dropPosition, quaternion.identity);
       OnItemDropEventCall(itemSO);
    }
    public override string GetNameInteraction()
    {
        return "Pegar";
    }
}
