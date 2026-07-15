using UnityEngine;

public class Porcao : BaseItem
{
    [SerializeField]private ItemSO porcaoSO;
    public override void PickUp()
    {
       
        Debug.Log("Porção usada!");
        
    }
}
