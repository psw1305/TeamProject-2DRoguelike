using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    protected int _id;
    protected string _name;
    protected int _maxHp;
    protected int _currentHp;
    protected float _range; // 사정거리
    protected float _movementSpeed;
    protected float _attackSpeed;
    protected int _attackDamage;

    protected Player _target;
    protected NavMeshAgent _agent;
    protected Coroutine _stateCoroutine;
    protected RaycastHit2D _rayHit;


    protected enum EnemyState
    {
        Live,
        Dead
    }
    protected EnemyState enemyState;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _stateCoroutine = null;

        enemyState = EnemyState.Live;
    }

    protected virtual void OnEnable()
    {
        _target = Main.Game.Player;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    protected void StopStateCoroutin()
    {
        if (_stateCoroutine != null)
        {
            StopCoroutine(_stateCoroutine);
            _stateCoroutine = null;
        }
    }

    protected void Damaged(int damage)
    {
        _currentHp -= damage;

        if (_currentHp <= 0)
        {
            enemyState = EnemyState.Dead;
            StopAllCoroutines();

            Destroy(gameObject);

            Debug.Log("주금");
        }

    }


    #region Util


    protected bool IsTargetStraight() // 벽에 가려지지않은 플레이어를 보고있는지 체크
    {
        _rayHit = Physics2D.Raycast(transform.position, DirectionToTarget());

        if (_rayHit && _rayHit.collider.gameObject.CompareTag("Player"))
        {
            return true;
        }

        if (_rayHit.collider.gameObject.CompareTag("Enemy") || _rayHit.collider.gameObject.CompareTag("EnemyBullet"))
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
    protected float AngleToTarget()
    {
        return Mathf.Atan2(DirectionToTarget().y, DirectionToTarget().x) * Mathf.Rad2Deg;
    }



    void OnDrawGizmosSelected() // 사정거리보기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }


    #endregion

}