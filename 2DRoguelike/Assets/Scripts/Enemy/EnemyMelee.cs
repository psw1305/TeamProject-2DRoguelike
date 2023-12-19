using System.Collections;
using UnityEngine;

public class EnemyMelee : Enemy
{

    protected override void Awake()
    {
        base.Awake();

        _maxHp = 50;
        _movementSpeed = 1.5f;
        _attackSpeed = 1;
        _range = 1.2f;

    }
    protected override void OnEnable()
    {
        base.OnEnable();
        Initialize();
    }

    void Update()
    {
        if (enemyState != EnemyState.live) return;

        Move();
    }

    void Initialize()
    {
        _currentHp = _maxHp;

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

            yield return new WaitForSeconds(_attackSpeed);

            _target.Damaged(_attackDamage);
        }
    }


}
