
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
public Image Icon;
public string itemName;
public bool isStakable;
public bool isUsable;
public BaseItem use;
}
