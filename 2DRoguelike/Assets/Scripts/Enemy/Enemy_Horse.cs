using UnityEngine;

public class Enemy_Horse : Enemy
{
    #region Fields

    private float _runTime = 0;
    private Vector3 _pos;

    #endregion

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
        _pos = _target.transform.position;
    }


    void Move()
    {
        if (_target == null) return;

        _agent.SetDestination(_pos);
        _runTime += Time.deltaTime;

        // 움직이는 시간이 3초 이상인가? => 해당 타겟 쫒아감, 그리고 초기화
        if (_runTime > 3f)
        {
            _animator.SetBool(IsWalkHash, true);
            StopStateCoroutin();
            _pos = _target.transform.position;
            _runTime = 0;
        }
    }
}