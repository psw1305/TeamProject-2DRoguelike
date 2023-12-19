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
        if(_blueprint.ItemAnimationController != null)
            GetComponentInChildren<Animator>().runtimeAnimatorController = _blueprint.ItemAnimationController;
        GetComponent<CircleCollider2D>().radius = _blueprint.GetRadius;
    }
}
