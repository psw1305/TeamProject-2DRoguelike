using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "PickupItem", menuName = "Blueprint/PickupItem")]
public class ItemBlueprint : ScriptableObject
{
    [Header("Basic")]
    public string itemName;
    public int itemAmount;
    public RuntimeAnimatorController itemAnimationController;
    public float getRadius;


    [Header("Item Type")]
    public ItemType itemType;
}