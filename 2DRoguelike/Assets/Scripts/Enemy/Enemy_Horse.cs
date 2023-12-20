using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class Enemy_Horse : Enemy
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Initialize();
    }

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


    float runTime = 0;
    Vector3 pos;

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

        Vector3 RandomPosition()
        {
            Vector3 randomDirection = transform.position + Random.insideUnitSphere * enemySO._range;

            // randomDirection.z = 0f;
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, enemySO._range, NavMesh.AllAreas)) // 위치가 NavMesh 안이라면 true
            {
                Debug.Log("충돌");
                return hit.position;
            }

            return transform.position;
        }

    }
}