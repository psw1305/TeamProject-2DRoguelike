using UnityEngine;

[CreateAssetMenu(fileName = "PickupItem", menuName = "Blueprint/PickupItem")]
public class ItemBlueprint : ScriptableObject
{
    [Header("Basic")]
    public string itemName;
    public HowItemGet howItemGet;
    public ItemType itemType;
    public int itemAmount;
    public Sprite itemSprite;
    public float getRadius;

    // 삭제 필요
    [Header("Automatic")]
    public float magnetRadius;
    public float magnetSpeed;
}
// Dropable, Interactable