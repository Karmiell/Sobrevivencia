using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventario_Sloot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
private const string VAZIO = "VAZIO";

[SerializeField]private TextMeshProUGUI slootHoldingText;
[SerializeField]private TextMeshProUGUI slootHoldingQuantidade;
[SerializeField]private TextMeshProUGUI itemName;
[SerializeField]private TextMeshProUGUI itemQuantidade;
[SerializeField]private LayerMask layerMaskUI;
[SerializeField]private RectTransform dragObject;
[SerializeField]private GameObject clone;
[SerializeField]private Canvas canvas;

private ItemSO itemSO = default;
private SlootUI slootUI = default;
private bool isHoldingSloot = false;
private Inventario_Sloot slootHolding;


    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }



    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hitInfo = CastRaycastFromRay(ray);
        if (!hitInfo.IsUnityNull())
        {
           
          if(hitInfo.TryGetComponent<Inventario_Sloot>(out var result))
            {
             if(result.GetSlootUI().GetDateDados() == default)return;
             slootHolding = result;
             isHoldingSloot = true;
             clone.SetActive(true);
             SetParmsClone();
            }  
        }
        else Debug.Log(hitInfo);
    }
public void OnDrag(PointerEventData pointerEventData)
    {
       
        if(!isHoldingSloot)return;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, pointerEventData.position, pointerEventData.pressEventCamera, out Vector3 localPoint);

        dragObject.position = localPoint;
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
               SwitchDateFromSlootSingle(slootHolding, result);
               ClearSlootHoldingParams();
               return; 
            }
        ClearSlootHoldingParams();
        return;
        }
        
    }

    private void SetParmsClone()
    {
        slootHoldingQuantidade.text = slootHolding.GetSlootUI().GetDateInt().ToString();
        slootHoldingText.text = slootHolding.GetSlootUI().GetDateString();
    }

    private void ClearSlootHoldingParams()
    {
        slootHolding = null;
        isHoldingSloot = false;
        clone.SetActive(false); 
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
