using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour {


    public Transform MoveTarget;

    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.destination = MoveTarget.position;        
    }

    public float GetRemainingDistance
    {
        get { return _navMeshAgent.remainingDistance; }
    }
}
