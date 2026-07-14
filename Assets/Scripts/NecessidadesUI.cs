using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class NecessidadesUI : MonoBehaviour
{
[SerializeField]private Image barLife;
[SerializeField]private TextMeshProUGUI textLife;
[SerializeField]private Image barHungry;
[SerializeField]private TextMeshProUGUI textHungry;


    private void OnEnable() => NecessidadesBasicas.OnStatsChange += NecessidadesBasicas_OnStatsChange;
    private void OnDisable() => NecessidadesBasicas.OnStatsChange -= NecessidadesBasicas_OnStatsChange;
 

    private void NecessidadesBasicas_OnStatsChange(int vidaAtual ,int vidaMax, float fomeAtual, float fomeMax)
    {
        barLife.fillAmount = (float)vidaAtual/vidaMax;
        barHungry.fillAmount = fomeAtual/fomeMax;

        textLife.text = (vidaAtual + "/" + vidaMax).ToString();
        textHungry.text = ($"{(int)fomeAtual}/{fomeMax}").ToString();
    }
}
