using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventario_Sloot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]private LayerMask layerMaskUI;
   private ItemSO itemSO = default;
   private SlootUI slootUI = default;
   private const string VAZIO = "VAZIO";
private bool isHoldingSloot = false;
private Inventario_Sloot slootholding;
private GameObject clone;


   [SerializeField]private TextMeshProUGUI itemName;
   [SerializeField]private TextMeshProUGUI itemQuantidade;


 public void OnBeginDrag(PointerEventData pointerEventData)
    {
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hitInfo = CastRaycastFromRay(ray);
        if (!hitInfo.IsUnityNull())
        {
           
          if(hitInfo.TryGetComponent<Inventario_Sloot>(out var result))
            {
             if(result.GetSlootUI().GetDateDados() == default)return;

                slootholding = result;
                isHoldingSloot = true;
                clone = Instantiate(gameObject, transform.position, Quaternion.identity);

            }  
        }
        else Debug.Log(hitInfo);
    }
public void OnDrag(PointerEventData pointerEventData)
    {
       
        if(!isHoldingSloot)return;
        
        clone.transform.localPosition += Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
public void OnEndDrag(PointerEventData pointerEventData)
    {
        if(!isHoldingSloot)return;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hitInfo = CastRaycastFromRay(ray);
        if (hitInfo.IsUnityNull())
        { 
        ClearSlootHoldingParams();
        return;
        }
        else
        { 
            if(hitInfo.TryGetComponent<Inventario_Sloot>(out var result))
            {
                if(result == this)
                {
                    ClearSlootHoldingParams();
                    return;
                }
               SwitchDateFromSlootSingle(slootholding, result);
               ClearSlootHoldingParams();
               return; 
            }
        ClearSlootHoldingParams();
        return;
        }
        
    }


    private void ClearSlootHoldingParams()
    {
        slootholding = null;
        isHoldingSloot = false;
        Destroy(clone.gameObject); 
    }

public void SwitchDateFromSlootSingle(Inventario_Sloot oldSloot, Inventario_Sloot newSloot)
    {
        var oldPosition = oldSloot.GetSlootUI();
        var newPosition = newSloot.GetSlootUI();
       
        oldSloot.SetDateFromSlootUI(newPosition);
        newSloot.SetDateFromSlootUI(oldPosition);
    }

   public void SetDateFromSlootUI(SlootUI slootUI)
    {
        this.slootUI = slootUI;
        slootUI.SetSlootSingle(this);
        itemSO = slootUI.GetDateDados();
        itemName.text = slootUI.GetDateString();
        if(slootUI.GetDateString().ToUpper() == VAZIO)itemQuantidade.text = "0";
        else itemQuantidade.text = slootUI.GetDateInt().ToString();
        
    }

    public SlootUI GetSlootUI() => slootUI;

    private GameObject CastRaycastFromRay(Ray ray)
    {
        if(Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, layerMaskUI))
        {
            return hitInfo.collider.gameObject;
        }
        return null;
    }
}
