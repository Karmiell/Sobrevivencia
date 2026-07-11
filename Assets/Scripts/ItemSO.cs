
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
public Sprite Icon;
public string itemName;
public bool isStakable;
public BaseItem use;

    public override string ToString()
    {
        return itemName;
    }
}
