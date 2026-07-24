using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MeseDeTrabalho : MonoBehaviour, IInteracteble
{
    private Stack<Image> windowStack;
    [SerializeField]private Image windowInstance;

    public void Interact()
    {
        ShowWindown();
    }
    public void ExitInteract()
    {
        HideWindown();
    }
    
    private void ShowWindown()
    {
        windowStack.Push(windowInstance);
    }
    private void HideWindown()
    {
        windowStack.Pop();
    }




}
