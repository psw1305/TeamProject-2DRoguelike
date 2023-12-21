using System.Collections;
using UnityEngine;

public class Enemy_Fly : Enemy
{

    protected override void OnEnable()
    {
        base.OnEnable();
        Initialize();
    }

    void Update()
    {
        if (_enemyState != EnemyState.live) return;

        Move();
    }

    void Initialize()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = Speed;
        _agent.stoppingDistance = AttackRange;

        CurrentHp = MaxHp;
    }



    void Move()
    {
        if (_target == null) return;

        _agent.SetDestination(_target.transform.position);

        if (_agent.velocity.magnitude > 0.2f) // 움직이는 중이면 true
        {
            _animator?.SetBool(IsWalkHash, true);
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
            _agent.stoppingDistance = Mathf.Clamp(_agent.stoppingDistance - 0.1f, 1.2f, AttackRange);
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

            _animator?.SetBool(IsWalkHash, false);
            _animator?.SetTrigger(AttackHash);

            _target.Damaged(transform, AttackDamage);
        }
    }

}
