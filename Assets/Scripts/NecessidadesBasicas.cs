using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class NecessidadesBasicas : MonoBehaviour
{
public static NecessidadesBasicas Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

public static event Action<bool> OnStarving;
public static event Action<int,int,float,float> OnStatsChange;


[SerializeField]private int vidaMax = 200;
[SerializeField]private float fomeMax = 200f;
[SerializeField]private float starvingLimit = 10f;
[SerializeField]private float modificadorFome = 0.1f;
[SerializeField]private int DamageAndTimeMinForStavingDamage = 5;

private float fomeAtual;
private float timeratual = 5f;
private int vidaAtual;
private int amount;

    private void Start()
    {
        MyReset();
        
        amount = DamageAndTimeMinForStavingDamage;
    }

    private void Update()
    {
        OnStatsChangeEventCall();
        if(NormalHungry())FomeCounter();
        if(IsStarving())DamageFromStarving();
    }
private void FomeCounter()
    {
        fomeAtual -= Time.deltaTime * modificadorFome;
        fomeAtual = math.clamp(fomeAtual, starvingLimit,fomeMax);
    }
private void DamageFromStarving()
    {
        if(CountDown())return;
        Damage_Heal(amount);
        timeratual = 5f;
    }   


public static void OnStatsChangeEventCall()
    {
        OnStatsChange?.Invoke(NecessidadesBasicas.Instance.GetVidaAtual(),NecessidadesBasicas.Instance.GetVidaMax(),NecessidadesBasicas.Instance.GetFomeAtual(),NecessidadesBasicas.Instance.GetFomeMax());
    }

private int GetVidaMax() => vidaMax;
private int GetVidaAtual() => vidaAtual;
private float GetFomeMax() => fomeMax;
private float GetFomeAtual() => fomeAtual;

public void Eat(float amount)
    {
        fomeAtual += amount;
        fomeAtual = math.clamp(fomeAtual, 0, fomeMax);
    }
public void Damage_Heal(int amount)
    {
        vidaAtual -= amount;
        vidaAtual = math.clamp(vidaAtual, 0, vidaMax);
    }    

public static void ChangeLife(int amount)
    {
        NecessidadesBasicas.Instance.Damage_Heal(amount);
        NecessidadesBasicas.OnStatsChangeEventCall();
        
    } 

private bool CountDown()
    {
        timeratual -= Time.deltaTime;
        if(timeratual >= 0) return true;
        return false;
    }

private bool IsStarving()
{
    if(fomeAtual <= starvingLimit)
    {
      OnStarving?.Invoke(true);
      return true;      
    }
    return false;
}
private bool NormalHungry()
{
    if(fomeAtual > starvingLimit)
        {
            OnStarving?.Invoke(false);
            return true;
        }
        return false;
}

private void MyReset()
    {
       vidaAtual = vidaMax;
       fomeAtual = fomeMax; 
    }
    
}
