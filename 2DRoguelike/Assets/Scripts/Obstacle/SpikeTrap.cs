using System;
using System.Collections;
using UnityEngine;

public class SpikeTrap : Obstacle
{
    public int trapAttack;
    public float TrapActiveDelay;
    private float currTime = 0;
    private Animator animator;

    private void Start()
    {
        mainObj = transform.parent.gameObject;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currTime += Time.deltaTime;
        }
        if (collision.CompareTag("Player") && currTime >= TrapActiveDelay)
        {
            currTime = 0;
            // 이곳에 플레이어가 데미지 받는 것을 구현하면 됨
            animator.SetBool("isAttack", true);
            Debug.Log(currTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currTime = 0;
        animator.SetBool("isAttack", false);
    }
}
