using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    [SerializeField] protected ItemBlueprint blueprint;
    [SerializeField] private Animator animator;

    public virtual void SetItem(ItemBlueprint blueprint)
    {
        this.blueprint = blueprint;

        if (this.blueprint.ItemAnimationController != null)
        {
            animator.runtimeAnimatorController = this.blueprint.ItemAnimationController;
        }
    }
}
