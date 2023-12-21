using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Plus : Enemy
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Initialize();
    }

    void Initialize()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = Speed;
        _agent.stoppingDistance = AttackRange;
    }


    void Update()
    {
        if (_enemyState != EnemyState.live) return;

        Move();
    }

    void Move()
    {
        _agent.SetDestination(_target.transform.position);

        if (_agent.velocity.magnitude > 0.2f) // 움직이는 중이면 true
        {
            StopStateCoroutin();
        }
        else
        {
            TakeAim();
        }
    }


    protected void TakeAim()
    {
        if (_attackCoroutine != null)
            return;

        if (!IsTargetStraight())
        {
            _agent.stoppingDistance = Mathf.Clamp(_agent.stoppingDistance - 0.1f, 1, AttackRange);
            return;
        }

        _agent.stoppingDistance = AttackRange; // 시야거리 초기화
        _attackCoroutine = StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.7f);

        while (true)
        {
            if (!IsTargetStraight())
                StopStateCoroutin();


            yield return new WaitForSeconds(AttackSpeed);

            Circle(4, BulletSpeed);


            yield return new WaitForSeconds(AttackSpeed);

            Circle(5, BulletSpeed);

        }
    }

}
