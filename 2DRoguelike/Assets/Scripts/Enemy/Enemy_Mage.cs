using System.Collections;
using UnityEngine;

public class Enemy_Mage : Enemy
{
    #region Init

    private void Initialize()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = Speed;
        _agent.stoppingDistance = AttackRange;
    }

    #endregion

    #region MonoBehaviour

    protected override void OnEnable()
    {
        base.OnEnable();
        Initialize();
    }

    private void Update()
    {
        if (_enemyState != EnemyState.live) return;

        Move();
    }

    #endregion

    #region Enemy Pattern Methods

    private void Move()
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

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.7f);

        while (true)
        {
            if (!IsTargetStraight())
                StopStateCoroutin();

            yield return new WaitForSeconds(AttackSpeed);

            FanShape(1, BulletSpeed);
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

    #endregion
}
