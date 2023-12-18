using System;
using System.Collections;
using UnityEngine;

public class SpikeTrap : Obstacle
{
    [SerializeField] private LayerMask activeTargetLayer;

    [SerializeField] private int trapDamage;
    [SerializeField] private float trapActiveDelay;
    private Animator _animator;


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (activeTargetLayer.value == (activeTargetLayer | (1 << collision.gameObject.layer)))
        {
            Invoke("ActiveTrap", trapActiveDelay);
        }
        base.OnTriggerEnter2D(collision);
    }

    private void ActiveTrap()
    {
        _animator.SetBool("isActive", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _animator.SetBool("isActive", false);
        CancelInvoke();
    }

}
