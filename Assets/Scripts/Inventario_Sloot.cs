using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Inventario_Sloot : MonoBehaviour
{
   private ItemSO itemSO = default;
   [SerializeField]private TextMeshProUGUI itemName;
   [SerializeField]private TextMeshProUGUI itemQuantidade;

   public void SetDateFromSlootUI(SlootUI slootUI)
    {
        itemSO = slootUI.GetDateDados();
        itemName.text = slootUI.GetDateString();
        itemQuantidade.text = slootUI.GetDateInt().ToString();
        if(!itemSO.IsUnityNull())itemQuantidade.text = (int.Parse(itemQuantidade.text) + 1).ToString();
    }
 
}
