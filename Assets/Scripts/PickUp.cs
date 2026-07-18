using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PickUp_Drop : BaseItem
{
[SerializeField]private ItemSO itemSO;
[SerializeField]private bool IsHandPlace;

  

    public override void PickUp()
    {
       OnItemPickEvent(itemSO);

       InteractionSelector.Instance.RemoveItemFromListUsable(this);

       if(IsHandPlace) Destroy(gameObject);
       else Addressables.ReleaseInstance(gameObject);
    }
    public override void Drop(Transform player)
    {
       var dropPosition = player.transform.position + player.transform.forward;
       itemSO.visual.InstantiateAsync(dropPosition, Quaternion.identity);
       OnItemDropEventCall(itemSO);
    }




    public override string GetNameInteraction()
    {
        return "Pegar";
    }
    
}
