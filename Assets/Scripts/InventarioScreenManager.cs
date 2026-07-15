using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class InventarioScreenManager : MonoBehaviour
{
[SerializeField]private Transform screenConteiner;
[SerializeField]private Transform sloot_empty;
[SerializeField]private Inventario_Sloot[] slootSingleArray;  
[SerializeField]private int inventarioSize = 8;
private static SlootUI[] inventarioArrayData;



    private void Start()
    {
        inventarioArrayData = new SlootUI[inventarioSize];
        slootSingleArray = new Inventario_Sloot[inventarioSize];
        CreateAtualItemSlootDate();
        BaseItem.OnItemPick += BaseItem_OnItemPick;
        PlayerScript.Instance.OnItemQuantidadeChange += PlayerScript_OnItemQuantidadeChange;
    }
    private void OnDisable()
    {
    BaseItem.OnItemPick -= BaseItem_OnItemPick;
    PlayerScript.Instance.OnItemQuantidadeChange -= PlayerScript_OnItemQuantidadeChange; 
    }

    private void PlayerScript_OnItemQuantidadeChange(ItemSO itemSO)
    {
        foreach(var atual in inventarioArrayData)
        {
            if(atual.GetDateDados() == itemSO)atual.SetQuantidadeUP();
        }
    
    }

    private void BaseItem_OnItemPick(object sender, BaseItem.OnItemPickEventArgs e)
    {
        UpdateDate();
        UpdateVisual();
      
    }

    private void UpdateDate()
    {
    var playerInvetario = PlayerScript.Instance.GetInvetario();
      for(int i = 0; i < inventarioArrayData.Length; i++)
        {
        var itemSOValid = (i < playerInvetario.Count)? playerInvetario[i] : default;
        inventarioArrayData[i].SetDateFromItemSO(itemSOValid);   
        }
    }

    private void UpdateVisual()
    {
        for(int i = 0 ; i < inventarioSize; i++)
        {
            slootSingleArray[i].SetDateFromSlootUI(inventarioArrayData[i]);
        }
    }


    private void CreateAtualItemSlootDate()
    {
     for(int i = 0 ; i < inventarioSize; i++)
        {
          inventarioArrayData[i] = new SlootUI();
          var slootEmpty = Instantiate(sloot_empty, screenConteiner);
          slootSingleArray[i] = slootEmpty.GetComponent<Inventario_Sloot>();
          slootSingleArray[i].SetDateFromSlootUI(inventarioArrayData[i]);
        }
    }

public static SlootUI[] GetInventarioData() => inventarioArrayData;
}




[Serializable]
public class SlootUI
{
    private string nameItem;
    private int quantidadeItem;
    private ItemSO dados;
    private Inventario_Sloot mySloot;

    public SlootUI(Inventario_Sloot mySloot = default ,int quantidadeItem = 1,string nameItem = "Vazio", ItemSO dados = default)
    {
        this.mySloot = mySloot;
        this.nameItem = nameItem;
        this.quantidadeItem = quantidadeItem;
        this.dados = dados;
    }
    public void SetDateFromItemSO(ItemSO itemSO = default)
    {
        if(itemSO == default)
        {
            return;
        }
        nameItem = itemSO.itemName;
        dados = itemSO; 
       
    }
    public void SetQuantidadeUP() => quantidadeItem++;
    public void SetQuantidadeDown() => quantidadeItem--;
    public void SetSlootSingle(Inventario_Sloot sloot) => mySloot = sloot; 
    public void ClearSlootUIDate()
    {
        nameItem = "Vazio";
        quantidadeItem = 1;
        dados = default;
    }


    public Inventario_Sloot GetMySlootSingle() => mySloot;
    public ItemSO GetDateDados() => dados;
    public int GetDateInt() => quantidadeItem;
    public string GetDateString() => nameItem;

}