using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    protected ItemBlueprint _blueprint;

    public virtual void SetItem(ItemBlueprint blueprint)
    {
        _blueprint = blueprint;
        if(_blueprint.itemAnimationController != null)
            GetComponentInChildren<Animator>().runtimeAnimatorController = _blueprint.itemAnimationController;
        GetComponent<CircleCollider2D>().radius = _blueprint.getRadius;
    }
}
