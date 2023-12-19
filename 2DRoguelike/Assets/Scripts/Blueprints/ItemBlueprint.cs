using UnityEngine;

[CreateAssetMenu(fileName = "PickupItem", menuName = "Blueprint/PickupItem")]
public class ItemBlueprint : ScriptableObject
{
    [Header("Basic")]
    [SerializeField] private string itemName;
    [SerializeField] private int itemAmount;
    [SerializeField] private RuntimeAnimatorController itemAnimationController;

    [Header("Item Type")]
    [SerializeField] private ItemType itemType;

    #region Properties

    public string ItemName => itemName;
    public int ItemAmount => itemAmount;
    public RuntimeAnimatorController ItemAnimationController => itemAnimationController;
    public ItemType ItemType => itemType;

    #endregion
}