using System.Collections;
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
        if (_enemyState != EnemySO.EnemyState.live) return;

        Move();
    }

    void Initialize()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _movementSpeed;
        _agent.stoppingDistance = _range;
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
            _agent.stoppingDistance = Mathf.Clamp(_agent.stoppingDistance - 0.1f, 1.2f, _range);
            return;
        }

        _agent.stoppingDistance = _range; // 시야거리 초기화
        _attackCoroutine = StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (!IsTargetStraight())
                StopStateCoroutin();


            if (_maxHp * 0.10 >= _currentHp)
            {
                Circle(10, 10);

                yield return new WaitForSeconds(1f);

                FanShape(5, 14, 7);

                yield return new WaitForSeconds(1f);
            }
            else if (_maxHp * 0.30 >= _currentHp)
            {
                Circle(14,3);

                yield return new WaitForSeconds(1.4f);

            }
            else if (_maxHp * 0.50 >= _currentHp)
            {
                FanShape(1, 7, 10);

                yield return new WaitForSeconds(0.5f);

                FanShape(14, 7, 7, true);

                yield return new WaitForSeconds(1);

            }
            else if (_maxHp * 0.75 >= _currentHp)
            {
                FanShape(7, 40);
                yield return new WaitForSeconds(1);

            }
            else
            {
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
