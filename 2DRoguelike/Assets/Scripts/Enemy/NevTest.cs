using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NevTest : MonoBehaviour
{
    private NavMeshAgent _agent;
    private JEH_Player _target;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void OnEnable()
    {
        _target = FindObjectOfType<JEH_Player>(); // 임시
    }


       
        void Update()
    {
        _agent.SetDestination(_target.transform.position);

    }
}
