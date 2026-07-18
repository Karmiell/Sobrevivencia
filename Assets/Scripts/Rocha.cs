using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Rocha : MonoBehaviour,IInteracteble
{

    private int maxAmuntInteraction = 3;
    private int atualInteractionNumber = 0;

    [SerializeField]private AssetReference pedra;
    [SerializeField]private float raioInterno = .8f;
    [SerializeField]private float raioExterno = 2.5f;

    public void Interact()
    {
        if(atualInteractionNumber >= maxAmuntInteraction)return;
        SpawnarObjecto();
        atualInteractionNumber ++;
        Debug.Log($"Pedra\nNumero de Interações: {atualInteractionNumber}");
        if(atualInteractionNumber == maxAmuntInteraction)gameObject.SetActive(false);  
    }

    string IInteracteble.GetNameInteraction()
    {
        return "minerar";
    }    
    
    private void SpawnarObjecto()
    {
        var angulo = UnityEngine.Random.Range(0f, math.PI *2);
        var distancia = UnityEngine.Random.Range(raioInterno, raioExterno);

        var x = math.cos(angulo) * distancia;
        var z = math.sin(angulo) * distancia;

        var spawnPoint = new Vector3(x,1f,z) + transform.position;
        pedra.InstantiateAsync(spawnPoint,quaternion.identity);
    }
}
