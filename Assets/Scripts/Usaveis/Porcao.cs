using UnityEngine;

public class Porcao : BaseItem
{
    [SerializeField]private ItemSO porcaoSO;
    public override void Interact()
    {
        if(!porcaoSO.isUsable)return;
        Debug.Log("Porção usada!");
        
    }
}
