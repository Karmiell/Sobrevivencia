using UnityEngine;

public class Porcao : BaseItem
{
    [SerializeField]private ItemSO porcaoSO;
    public override void Interact()
    {
       
        Debug.Log("Porção usada!");
        
    }
}
