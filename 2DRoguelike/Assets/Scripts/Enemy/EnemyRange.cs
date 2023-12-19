using System.Collections;
using UnityEngine;

public class EnemyRange : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        _maxHp = 30;
        _movementSpeed = 1;
        _attackSpeed = 1;
        _range = 7;
        _bulletSpeed = 5;
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
        if (enemyState != EnemyState.live) return;

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
        if (_stateCoroutine != null)
            return;

        if (!IsTargetStraight())
        {
            _agent.stoppingDistance = Mathf.Clamp(_agent.stoppingDistance - 0.1f, 1, _range);
            return;
        }

        _agent.stoppingDistance = _range; // 시야거리 초기화
        _stateCoroutine = StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (!IsTargetStraight())
                StopStateCoroutin();

            yield return new WaitForSeconds(_attackSpeed);


            FanShape(1, _bulletSpeed);

            yield return new WaitForSeconds(_attackSpeed);

            FanShape(3, _bulletSpeed);

            yield return new WaitForSeconds(_attackSpeed);

          Circle(12, _bulletSpeed);

            yield return new WaitForSeconds(_attackSpeed);

            Circle(4, _bulletSpeed);
        }
    }

}


/*
 풀링 부분 백업

        FanShape(3, _bulletSpeed, false);

        EnemyProjectile enemyProjectile = Main.Object.Spawn<EnemyProjectile>("EenmyBullet", gameObject.transform.position);
        enemyProjectile.SetInfo(1, 7);//float 값이라 임의로 넣음
        enemyProjectile.transform.rotation = Quaternion.Euler(0, 0, AngleToTarget());

        enemyProjectile.SetVelocity(DirectionToTarget() * 5); //5에 발사체 스피드 넣어주시면 됩니다
        enemyProjectile.gameObject.tag = "EnemyProjectile";
 
 
 */