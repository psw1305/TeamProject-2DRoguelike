using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : BaseItem
{
    public void OnInteract()
    {
        Inventory.Instance.GetSpecial(_blueprint);
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
