using System.Collections;
using UnityEngine;

public class Chest : PickupItem
{
    private Animator _animator;
    private bool isAnimationPlaying;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private IEnumerator WaitingAnimationEnd()
    {
        while (isAnimationPlaying)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("End"))
            {
                isAnimationPlaying = false;
            }
            yield return null;
        }

        OpenChest();
    }

    public void OpenChest()
    {
        StopAllCoroutines();

        for (int i = 0; i < 3; i++)
        {
            Main.Reward.CreateBasicReward(gameObject.transform.position);
        }
            
        Destroy(gameObject);
    }

    protected override void PlayerItemPickup()
    {
        if (Main.Game.Player.UseKey())
        {
            _animator.SetTrigger("Open");
            isAnimationPlaying = true;
            StartCoroutine(WaitingAnimationEnd());
        }
    }
}
