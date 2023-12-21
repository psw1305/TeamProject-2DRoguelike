using UnityEngine;
using UnityEngine.AI;


public class FreeMovement : MonoBehaviour
{
    private  NavMeshAgent _agent;
    private  Vector3 _target; // 목적지

    private float _range = 4;

     void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }


     void OnEnable()
    {
        _target = RandomPosition();

    }

    private void Update()
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