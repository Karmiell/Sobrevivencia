using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
public static PlayerScript Instance;

 private GameObject interactebleSelect;
 private BaseItem usableSelect;
public event Action<ItemSO> OnItemQuantidadeChange;


 [SerializeField]private List<ItemSO> inventario;


   private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    inventario = new List<ItemSO>();

    }

    private void Start()
    {

    InputPlayerHandler.Instance.OnInterationPress += InputPlayerHandler_OnInterationPress;
    MovimentScript.OnAnyMovimentValue += MovimentScript_OnAnyMovimentValue;    
    BaseItem.OnItemPick += BaseItem_OnItemPick;
    }

    private void BaseItem_OnItemPick(object sender, BaseItem.OnItemPickEventArgs e)
    {
        if (inventario.Contains(e.itemSO))
        {
           foreach(var atual in inventario)
            {
                if(atual == e.itemSO)OnItemQuantidadeChange?.Invoke(atual);
            }
            return;
        }
        inventario.Add(e.itemSO);
    }

    private void MovimentScript_OnAnyMovimentValue(Vector2 moveValue)
    {
        if(moveValue != Vector2.zero)
        {
            var usable = InteractionSelector.Instance.GetItemUsable();
            var interacteble = InteractionSelector.Instance.GetInteractebles();
            interactebleSelect = interacteble;
            usableSelect = usable;
        }
    }

    private void InputPlayerHandler_OnInterationPress(int i)
    {
        switch (i)
        {
            case 1:
            HandleInteractionMain();
            break;

            case 2:
            HandleInteractionAlt();
            break;
        
        }
    }

    private void HandleInteractionMain()
    {
        if(interactebleSelect.IsUnityNull())return;

        interactebleSelect.GetComponent<IInteracteble>().Interact();
    }

    private void HandleInteractionAlt()
    {
      if(usableSelect.IsUnityNull())return;
      usableSelect.Interact();
    }
    
public GameObject GetInteractebleObject() => interactebleSelect;
public List<ItemSO> GetInvetario() => inventario;
}
