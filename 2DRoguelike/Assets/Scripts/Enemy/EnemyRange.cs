using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRange : MonoBehaviour
{

    #region Gizmo

    void OnDrawGizmosSelected() // 공격범위 보기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    #endregion

    #region Field

    private Player _target;

    private int _maxHp;
    private int _hp;
    private float _range; // 사정거리
    private float _movementSpeed;
    private float _attackSpeed;


    private NavMeshAgent _agent;
    private Coroutine _stateCoroutine = null;
    private RaycastHit2D _rayHit;

    #endregion

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _maxHp = 10;
        _movementSpeed = 1;
        _attackSpeed = 1;
        _range = 7;

        _target = Main.Game.Player;

    }

    private void OnEnable()
    {
        //   _target = FindObjectOfType<Player>(); // 임시

        _hp = _maxHp;

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _movementSpeed;
        _agent.stoppingDistance = _range;

    }


    void Update()
    {
        Move();
    }

    void Move()
    {
        if(_target == null) return;


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
            yield return new WaitForSeconds(_attackSpeed);

            RealizeAttack();

            if (!IsTargetStraight()) // 공격중에 플레이어가 벽뒤로 갈수있으니 공격할때마다 레이 체크.
                StopStateCoroutin();
        }
    }

    public EnemyBullet bullet;

    void RealizeAttack()
    {
        Instantiate(bullet, this.gameObject.transform)._target = DirectionToTarget();
        Debug.Log("원거리공격");

    }


    void StopStateCoroutin()
    {
        if (_stateCoroutine != null)
        {
            StopCoroutine(_stateCoroutine);
            _stateCoroutine = null;
        }
    }



    bool IsTargetStraight() // 벽에 가려지지않은 플레이어를 보고있는지 체크
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
