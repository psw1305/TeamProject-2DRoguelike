using System.Collections;
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
    /// 근접일때만 공격하는 적, 타겟 될때까지 이동하는 적, 이동하면서 공격 동시에 하는놈,
    /// 제자리에서 공격하는 적
    /// 
    /// 
    /// Acceleration 을 50정도 줘야 속도 주체못하는거 잡을수있음.
    /// </summary>


    #region Gizmo

    void OnDrawGizmosSelected() // 공격범위 보기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _visibility);
    }

    #endregion

    #region Field

    public JEH_Player _target;
    public float _visibility; // 공격사정거리
    public int _movingSpeed = 0;
    public int _attackSpeed = 0;



    private NavMeshAgent _agent;
    private Coroutine _stateCoroutine = null;
    private RaycastHit2D _rayHit;

    #endregion

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.stoppingDistance = _visibility;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        _agent.SetDestination(_target.transform.position);

        if (_agent.velocity.magnitude > 0.2f) // 움직이는 중이면 true
        {
            if (_stateCoroutine != null)
            {
                StopCoroutine(_stateCoroutine);
                _stateCoroutine = null;
            }
        }
        else
        {
            if (_stateCoroutine != null)
                return;


            if (IsTargetStraight())
                Attack();
            else
                _agent.stoppingDistance -= 1;

        }
    }

    #region Attack

    bool IsTargetStraight()
    {
        _rayHit = Physics2D.Raycast(transform.position, DirectionToTarget());

        if (_rayHit && _rayHit.collider.gameObject.CompareTag("Player"))
        {
            return true;
        }

        return false;
    }


    protected void Attack()
    {
        _agent.stoppingDistance = _visibility;
        _stateCoroutine = StartCoroutine(AttackCoroutin());
        Debug.Log("공격시작");
    }

    IEnumerator AttackCoroutin()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("적의공격");
        }
    }

    #endregion

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, _target.gameObject.transform.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (_target.gameObject.transform.position - transform.position).normalized;
    }


}