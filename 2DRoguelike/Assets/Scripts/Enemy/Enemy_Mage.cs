using System.Collections;
using UnityEngine;

public class Enemy_Mage : Enemy
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
        _agent.speed = _movementSpeed;
        _agent.stoppingDistance = _range;
    }


    void Update()
    {
        if (_enemyState != EnemySO.EnemyState.live) return;

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
            _agent.stoppingDistance = Mathf.Clamp(_agent.stoppingDistance - 0.1f, 1, _range);
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



            yield return new WaitForSeconds(_attackSpeed);

            FanShape(1, _bulletSpeed);


        }
    }

}
