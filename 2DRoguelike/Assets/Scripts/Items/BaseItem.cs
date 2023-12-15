using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    protected ItemBlueprint _blueprint;

    public virtual void SetItem(ItemBlueprint blueprint)
    {
        _blueprint = blueprint;
        GetComponentInChildren<SpriteRenderer>().sprite = _blueprint.itemSprite;
        GetComponent<CircleCollider2D>().radius = _blueprint.magnetRadius;
    }
}
