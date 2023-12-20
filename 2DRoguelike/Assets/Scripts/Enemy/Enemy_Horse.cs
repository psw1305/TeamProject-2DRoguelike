using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Horse : Enemy
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Initialize();
    }

    float runTime = 0;
    Vector3 pos;

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

        pos = _target.transform.position;
    }



    void Move()
    {
        if (_target == null) return;

        _agent.SetDestination(pos);


        runTime += Time.deltaTime;

        if (runTime > 3f) // 움직이는 중이면 true
        {
            _animator?.SetBool(isWalkHash, true);
            StopStateCoroutin();
            pos = _target.transform.position;
            runTime = 0;

        }
        else
        {
            // 말 공격은 몸통박치기
        }


    }
}