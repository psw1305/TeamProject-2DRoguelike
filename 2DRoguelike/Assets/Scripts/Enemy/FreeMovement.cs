using UnityEngine;
using UnityEngine.AI;


public class FreeMovement : MonoBehaviour
{
    #region Gizmo

    void OnDrawGizmosSelected() // 이동범위 보기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);

        Debug.DrawRay(_target, Vector3.up);

    }

    #endregion

    private NavMeshAgent _agent;
    private Vector3 _target; // 목적지

    private float _range; 


    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _range = 7;
    }

    private void OnEnable()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _target = RandomPosition();
    }


    void Update()
    {
        _agent.SetDestination(_target);

        if (Vector3.Distance(transform.position, _target) < 1f)
        {
            _target = RandomPosition();
        }
    }

    Vector3 RandomPosition()
    {
        Vector3 randomDirection = transform.position + Random.insideUnitSphere * _range;

       // randomDirection.z = 0f;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _range, NavMesh.AllAreas)) // 위치가 NavMesh 안이라면 true
        {
            return hit.position;
        }

        return transform.position;
    }
}