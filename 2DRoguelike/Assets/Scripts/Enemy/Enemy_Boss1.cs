using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy_Boss1 : Enemy
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Initialize();
    }

    void Update()
    {
        if (enemySO.enemyState != EnemySO.EnemyState.live) return;

        Move();
    }

    void Initialize()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = enemySO._movementSpeed;
        _agent.stoppingDistance = enemySO._range;
    }


    void Move()
    {
        if (_target == null) return;

        _agent.SetDestination(_target.transform.position);


        if (_agent.velocity.magnitude > 0.2f) // 움직이는 중이면 true
        {
            _animator?.SetBool(isWalkHash, true);
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
            _agent.stoppingDistance = Mathf.Clamp(_agent.stoppingDistance - 0.1f, 1.2f, enemySO._range);
            return;
        }

        _agent.stoppingDistance = enemySO._range; // 시야거리 초기화
        _attackCoroutine = StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (!IsTargetStraight())
                StopStateCoroutin();


            if (enemySO._maxHp * 0.10 >= enemySO._currentHp)
            {
                Circle(20, 5);

                yield return new WaitForSeconds(1f);

                Circle(16, 7);

                yield return new WaitForSeconds(1f);
            }
            else if (enemySO._maxHp * 0.25 >= enemySO._currentHp)
            {
                Circle(14,3);

                yield return new WaitForSeconds(1.4f);

            }
            else if (enemySO._maxHp * 0.50 >= enemySO._currentHp)
            {
                _animator?.SetTrigger(AttackHash);
                FanShape(1, 7, 10);

                yield return new WaitForSeconds(0.5f);

                _animator?.SetTrigger(AttackHash);
                FanShape(14, 7, 7, true);

                yield return new WaitForSeconds(1);

            }
            else if (enemySO._maxHp * 0.75 >= enemySO._currentHp)
            {
                _animator?.SetTrigger(AttackHash);
                FanShape(7, 40);
                yield return new WaitForSeconds(1);

            }
            else
            {
                _animator?.SetTrigger(AttackHash);
                FanShape(3, 20);

                yield return new WaitForSeconds(1.3f);

                FanShape();
                yield return new WaitForSeconds(0.3f);
                FanShape();
                yield return new WaitForSeconds(0.3f);
                FanShape();

                yield return new WaitForSeconds(2f);
            }




        }
    }


}
