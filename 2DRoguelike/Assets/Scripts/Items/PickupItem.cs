using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PickupItem : BaseItem
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) PlayerGet();
    }

    protected virtual void PlayerGet()
    {
        if(Inventory.Instance.GetItem(_blueprint.itemType, _blueprint.itemAmount))
        {
            Destroy(gameObject);
        }
    }
}
