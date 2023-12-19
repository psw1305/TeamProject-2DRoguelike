using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : BaseItem
{
    public override void SetItem(ItemBlueprint blueprint)
    {
        base.SetItem(blueprint);
        InteractableItemBluePrint target = blueprint as InteractableItemBluePrint;
        GetComponentInChildren<SpriteRenderer>().sprite = target.itemSprite;
    }
    public void OnInteract()
    {
        Inventory.Instance.GetSpecial(blueprint);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;

        other.GetComponent<ItemInteract>().CurrentItem = this;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;

        ItemInteract itemInteract = other.GetComponent<ItemInteract>();
        if(itemInteract.CurrentItem == this) itemInteract.CurrentItem = null;
    }
}
