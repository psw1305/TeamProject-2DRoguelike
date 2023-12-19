using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region Gizmo

    void OnDrawGizmosSelected() // 공격범위 보기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    #endregion


    protected Player _target;

    protected int _maxHp;
    protected int _hp;
    protected float _range; // 사정거리
    protected float _movementSpeed;
    protected float _attackSpeed;


    protected NavMeshAgent _agent;
    protected Coroutine _stateCoroutine = null;
    protected RaycastHit2D _rayHit;



    protected virtual  void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void OnEnable()
    {
        _hp = _maxHp;

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _movementSpeed;
        _agent.stoppingDistance = _range;

    }



    protected bool IsTargetStraight() // 벽에 가려지지않은 플레이어를 보고있는지 체크
    {
        _rayHit = Physics2D.Raycast(transform.position, DirectionToTarget());

        if (_rayHit && _rayHit.collider.gameObject.CompareTag("Player"))
        {
            return true;
        }

        return false;
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, _target.gameObject.transform.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (_target.gameObject.transform.position - transform.position).normalized;
    }


}