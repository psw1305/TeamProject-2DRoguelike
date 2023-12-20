using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class InteractableItem : BaseItem
{
    protected EquipSystem _equipSystem = new EquipSystem();
    protected InteractableItemBluePrint targetBp;

    public override void SetItem(ItemBlueprint blueprint)
    {
        base.SetItem(blueprint);
        targetBp = blueprint as InteractableItemBluePrint;
        GetComponentInChildren<SpriteRenderer>().sprite = targetBp.itemSprite;
    }

    // TODO => 충돌시 바로 구매 후 스탯 반영이 되도록
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        _equipSystem.GetItemStatus(targetBp);
        // TODO => 소리 출력
        Destroy(gameObject);
    }
}
