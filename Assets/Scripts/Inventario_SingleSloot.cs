using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Inventario_Sloot : MonoBehaviour
{
   private ItemSO itemSO = default;
   private const string VAZIO = "VAZIO";
   [SerializeField]private TextMeshProUGUI itemName;
   [SerializeField]private TextMeshProUGUI itemQuantidade;

   public void SetDateFromSlootUI(SlootUI slootUI)
    {
        itemSO = slootUI.GetDateDados();
        itemName.text = slootUI.GetDateString();
        if(slootUI.GetDateString().ToUpper() == VAZIO)itemQuantidade.text = "0";
        else itemQuantidade.text = slootUI.GetDateInt().ToString();
        
        
    }
}
