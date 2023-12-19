using UnityEngine;

[CreateAssetMenu(fileName = "PickupItem", menuName = "Blueprint/PickupItem")]
public class ItemBlueprint : ScriptableObject
{
    [Header("Basic")]
    [SerializeField] private string itemName;
    [SerializeField] private int itemAmount;
    [SerializeField] private RuntimeAnimatorController itemAnimationController;
    [SerializeField] private float getRadius;

    [Header("Item Type")]
    [SerializeField] private ItemType itemType;

    #region Properties

    public string ItemName => itemName;
    public int ItemAmount => itemAmount;
    public RuntimeAnimatorController ItemAnimationController => itemAnimationController;
    public float GetRadius => getRadius;
    public ItemType ItemType => itemType;

    #endregion
}