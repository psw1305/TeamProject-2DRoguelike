using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 메모
    /// 
    /// 
    ///  안움직이는 몹도 있을테니까 이동관련한건 다른스크립트로 빼야할듯
    ///  isStopped <- 움직임 멈춘지가아니라 머신 작동시켜놨는지 아닌지라 멈춘거 감지못함
    ///  멈췄을때 레이를 쏴보고 캐릭터 잡히면 공격, 다른게 잡히면 stoppingDistance 줄여서 더 이동. 캐릭터 감지되면 stoppingDistance 기본값으로 복원
    ///  
    /// 
    /// 
    /// Acceleration 을 50정도 줘야 속도 주체못하는거 잡을수있음.
    /// 자유 이동할떈 speed 낮고 Acceleration를 5정도 줘야 자연스럽게 방향 트는걸로 보임
    /// 
    /// 플레이어랑 거리로 거리 좁힐지말지 보니까 겜 시작할때 게임오브젝트 만들어지는 순간에도 거리변경 뜬다.
    /// 
    /// 
    /// _range 가 1 이상이여야 캐릭터와 몹이 중첩되는 문제 피할수있음. docs에서도 stoppingDistance은 정확하지 않다고한다....
    /// 
    /// 
    /// </summary>


    #region Gizmo

    void OnDrawGizmosSelected() // 공격범위 보기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    #endregion

    #region Field

    private JEH_Player _target;

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
        _movementSpeed = 2;
        _attackSpeed = 1;
        _range = 6;

    }

    private void OnEnable()
    {
        _target = FindObjectOfType<JEH_Player>(); // 임시

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
        //transform.Translate(DirectionToTarget() * Time.deltaTime);

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
            _agent.stoppingDistance = Mathf.Clamp(_agent.stoppingDistance - 1, 1, _range);
            return;
        }

        _agent.stoppingDistance = _range; // 시야거리 초기화
        _stateCoroutine = StartCoroutine(AttackCoroutin());
        Debug.Log("적의 공격시작");
    }

    IEnumerator AttackCoroutin()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackSpeed);
            Debug.Log("적의 공격");

            if (!IsTargetStraight()) // 공격중에 플레이어가 벽뒤로 갈수있으니 공격할때마다 레이 체크.
                StopStateCoroutin();
        }
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