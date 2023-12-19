using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
        while(isAnimationPlaying)
        {
            if(_animator.GetCurrentAnimatorStateInfo(0).IsName("End"))
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
        Inventory.Instance.UseKey();

        for(int i=0; i < _blueprint.itemAmount; i++)
            RewardManager.Instance.CreateBasicReward(gameObject.transform.position);
            
        Destroy(gameObject);
    }

    protected override void PlayerGet()
    {
        if(Inventory.Instance.Key >= 1)
        {
            _animator.SetTrigger("Open");
            isAnimationPlaying = true;
            StartCoroutine(WaitingAnimationEnd());
        }
    }
}
