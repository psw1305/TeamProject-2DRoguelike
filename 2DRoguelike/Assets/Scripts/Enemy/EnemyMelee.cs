using System.Collections;
using UnityEngine;

public class EnemyMelee : Enemy
{


    protected override void Awake()
    {
        base.Awake();

        _maxHp = 10;
        _movementSpeed = 1.5f;
        _attackSpeed = 1;
        _range = 1.2f;

        _target = Main.Game.Player;
    }

    protected override void OnEnable()
    {
        base.OnEnable();


    }



    void Update()
    {
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
            Attack();
        }
    }


    protected void Attack()
    {
        if (_stateCoroutine != null)
            return;

        if (!IsTargetStraight())
        {
            _agent.stoppingDistance = Mathf.Clamp(_agent.stoppingDistance - 0.1f, 1.2f, _range);
            return;
        }

        _agent.stoppingDistance = _range; // 시야거리 초기화
        _stateCoroutine = StartCoroutine(AttackCoroutin());
    }

    IEnumerator AttackCoroutin()
    {
        while (true)
        {
            if (!IsTargetStraight()) // 공격중에 플레이어가 벽뒤로 갈수있으니 공격할때마다 레이 체크.
                StopStateCoroutin();

            yield return new WaitForSeconds(_attackSpeed);

            RealizeAttack();

       
        }
    }

    void RealizeAttack()
    {
        Debug.Log("물리공격");
    }


    void StopStateCoroutin()
    {
        if (_stateCoroutine != null)
        {
            StopCoroutine(_stateCoroutine);
            _stateCoroutine = null;
        }
    }


}
