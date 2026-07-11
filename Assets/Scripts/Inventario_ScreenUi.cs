using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Inventario_ScreenUi : MonoBehaviour
{
[SerializeField]private Transform screenConteiner;
[SerializeField]private Transform sloot_empty;
[SerializeField]private Inventario_Sloot[] slootSingleArray;  
[SerializeField]private int inventarioSize = 8;



private SlootUI[] inventarioArrayData;

    private void Start()
    {
        inventarioArrayData = new SlootUI[inventarioSize];
        slootSingleArray = new Inventario_Sloot[inventarioSize];
        CreateAtualItem();
        BaseItem.OnItemPick += BaseItem_OnItemPick;
        PlayerScript.Instance.OnItemQuantidadeChange += PlayerScript_OnItemQuantidadeChange;
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


    private void CreateAtualItem()
    {
     for(int i = 0 ; i < inventarioSize; i++)
        {
          inventarioArrayData[i] = new SlootUI();
          var slootEmpty = Instantiate(sloot_empty, screenConteiner);
          slootSingleArray[i] = slootEmpty.GetComponent<Inventario_Sloot>();
          slootSingleArray[i].SetDateFromSlootUI(inventarioArrayData[i]);
        }
    }
}

public class SlootUI
{
    private string nameItem;
    private int quantidadeItem;
    private ItemSO dados;

    public SlootUI(int quantidadeItem = 0,string nameItem = "Vazio", ItemSO dados = default)
    {
        this.nameItem = nameItem;
        this.quantidadeItem = quantidadeItem;
        this.dados = dados;
    }
    public void SetDateFromItemSO(ItemSO itemSO = default)
    {
        if(itemSO == default)return;
        nameItem = itemSO.itemName;
        dados = itemSO; 
    }
    public void SetQuantidadeUP()
    {
        quantidadeItem++;
    }

    public ItemSO GetDateDados() => dados;
    public int GetDateInt() => quantidadeItem;
    public string GetDateString() => nameItem;

}