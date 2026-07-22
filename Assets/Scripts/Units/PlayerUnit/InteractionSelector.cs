using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionSelector : MonoBehaviour
{
public static InteractionSelector Instance;

   
   [SerializeField] private List<GameObject> interacteblesList;
   [SerializeField] private List<BaseItem> usableList;

   private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        interacteblesList = new List<GameObject>();
        usableList = new List<BaseItem>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.TryGetComponent<IInteracteble>(out var result))
        {
            interacteblesList.Add(other.gameObject);
            Debug.Log($"Interação de {result.GetNameInteraction()} a vista!");
        }
        if(other.TryGetComponent<BaseItem>(out var item))
        {
            usableList.Add(item);
            Debug.Log($"Interação de {item.GetNameInteraction()} a vista!");
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<IInteracteble>(out var result))
        {
        if(interacteblesList.Contains(other.gameObject))interacteblesList.Remove(other.gameObject);    
        }
        if(other.TryGetComponent<BaseItem>(out var item) && usableList.Contains(item))
        {
            usableList.Remove(item);
        }
        
    }
    public GameObject GetInteractebles() => interacteblesList.FirstOrDefault();
    public BaseItem GetItemUsable() => usableList.LastOrDefault();
    
    public void RemoveItemFromListUsable(BaseItem item)
    {
        if(usableList.Contains(item))usableList.Remove(item);
    }
  
  
}
