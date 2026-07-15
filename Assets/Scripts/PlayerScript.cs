using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

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
    BaseItem.OnItemDrop += BaseItem_OnItemDrop;
    }

    private void Update()
    {
        if(!Keyboard.current.qKey.wasPressedThisFrame)return;
        if(inventario.Count == 0)return;
        inventario[0].use.Drop(transform);
    }

    private void OnDisable()
    {
    InputPlayerHandler.Instance.OnInterationPress -= InputPlayerHandler_OnInterationPress;
    MovimentScript.OnAnyMovimentValue -= MovimentScript_OnAnyMovimentValue;    
    BaseItem.OnItemPick -= BaseItem_OnItemPick;
    BaseItem.OnItemDrop -= BaseItem_OnItemDrop;
    }

    private void BaseItem_OnItemDrop(object sender, BaseItem.OnItemPickEventArgs e)
    {
        foreach(var atual in InventarioScreenManager.GetInventarioData())
        {
            if(atual.GetDateDados() == e.itemSO)
            {
                if(atual.GetDateInt() > 1)
                {
                    atual.SetQuantidadeDown();
                    atual.GetMySlootSingle().SetDateFromSlootUI(atual);
                    break;
                }
                else 
                {
                    inventario.Remove(atual.GetDateDados());
                    atual.ClearSlootUIDate();
                    atual.GetMySlootSingle().SetDateFromSlootUI(atual);
                    break;
                }
                
            }
        }
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
      usableSelect.PickUp();
    }
    
public GameObject GetInteractebleObject() => interactebleSelect;
public List<ItemSO> GetInvetario() => inventario;
}
