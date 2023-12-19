using System.Collections;
using UnityEngine;

public class EnemyRange : Enemy
{

    public EnemyBullet bullet;

    protected override void Awake()
    {
        base.Awake();

        // 자기 공격정보 넣어주기
        _maxHp = 10;
        _movementSpeed = 1;
        _attackSpeed = 1;
        _range = 7;

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Initialize();

    }

    void Initialize()
    {
        _currentHp = _maxHp;

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _movementSpeed;
        _agent.stoppingDistance = _range;
    }


    void Update()
    {
        if (enemyState == EnemyState.Dead) return;
        Move();
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
        if (_stateCoroutine != null)
            return;

        if (!IsTargetStraight())
        {
            _agent.stoppingDistance = Mathf.Clamp(_agent.stoppingDistance - 0.1f, 1, _range);
            return;
        }

        _agent.stoppingDistance = _range; // 시야거리 초기화
        _stateCoroutine = StartCoroutine(AttackCoroutin());
    }

    IEnumerator AttackCoroutin()
    {
        while (true)
        {
            if (!IsTargetStraight())
                StopStateCoroutin();

            yield return new WaitForSeconds(_attackSpeed);

            Attack();

        }
    }


    void Attack()
    {
        EnemyBullet obj = Instantiate(bullet);
        obj.gameObject.transform.SetParent(transform, false);
        obj.gameObject.transform.localRotation = Quaternion.Euler(0, 0, AngleToTarget());

        obj._bulletSpeed = 5;
        obj._damage = _attackDamage;

    }


}
