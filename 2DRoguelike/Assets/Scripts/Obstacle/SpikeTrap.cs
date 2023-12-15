using System;
using System.Collections;
using UnityEngine;

public class SpikeTrap : Obstacle
{
    [SerializeField] private int trapAttack;
    [SerializeField] private float trapActiveDelay;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("ActiveTrap", trapActiveDelay);
        }
    }
    
    private void ActiveTrap()
    {
        _animator.SetBool("isActive", true);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        _animator.SetBool("isActive", false);
    }
}
