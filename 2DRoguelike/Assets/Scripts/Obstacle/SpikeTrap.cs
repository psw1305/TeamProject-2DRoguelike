using System;
using System.Collections;
using UnityEngine;

public class SpikeTrap : Obstacle
{
    public int trapAttack;
    public float TrapActiveDelay;
    private float _currTime = 0;
    private Animator _animator;

    private void Start()
    {
        mainObj = transform.parent.gameObject;
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 플레이어가 접촉(Enter)하면 일정 코루틴 Or Invoke로 딜레이 주어 공격을 할 수 있는 매서드
    /// </summary>
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _currTime += Time.deltaTime;
        }
        if (collision.CompareTag("Player") && _currTime >= TrapActiveDelay)
        {
            _currTime = 0;
            // 이곳에 플레이어가 데미지 받는 것을 구현하면 됨
            _animator.SetBool("isAttack", true);
            Debug.Log(_currTime);
        }
    }

    /// <summary>
    /// 수정 후 주석 제거
    /// </summary>

    private void OnTriggerExit2D(Collider2D collision)
    {
        _currTime = 0;
        _animator.SetBool("isAttack", false);
    }
}
