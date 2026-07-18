using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;


[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
public Sprite Icon;
public string itemName;
public bool isStakable;
public BaseItem use;
public AssetReference visual;


public override string ToString()
    {
        return itemName;
    }
}
