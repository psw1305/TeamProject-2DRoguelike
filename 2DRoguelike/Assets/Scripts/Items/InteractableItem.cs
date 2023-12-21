using UnityEngine;

public class InteractableItem : BaseItem
{
    protected EquipSystem _equipSystem = new();
    protected InteractableItemBluePrint targetBp;

    public override void SetItem(ItemBlueprint blueprint)
    {
        base.SetItem(blueprint);
        targetBp = blueprint as InteractableItemBluePrint;
        GetComponentInChildren<SpriteRenderer>().sprite = targetBp.itemSprite;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;

        SFX.Instance.PlayOneShot(SFX.Instance.getPassiveItem);
        _equipSystem.GetItemStatus(targetBp);
        Destroy(gameObject);
    }
}
