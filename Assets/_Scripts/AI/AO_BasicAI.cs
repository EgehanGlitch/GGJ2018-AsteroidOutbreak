using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AO_BasicAI : MonoBehaviour {

    public Transform[] AIPath;

    public int _currentPathPoint = 0;

    public bool ShouldDoPath = true;

    public NavMeshAgent Agent;
    public float PanickedRunSpeed;
    public float WalkSpeed;

    public float Threshold = 0.2f;
    public bool LoopPatrol = false;
    [SerializeField] private bool _panicked = false;
    public bool Panicked
    {
        get { return _panicked; }
        set {
            if (value == false) return;
            if(_panicked != value)
            {
                //panicked event
                
                _panicked = value;
                BecomePanicked();
            }
            
        }
    }

    private AO_CharacterController _characterController;

    private bool _shouldUpdate = true;

    public bool _isPatroling = false;
    
    private void Awake()
    {
        _characterController = GetComponent<AO_CharacterController>();
        if (Agent == null) Agent = GetComponent<NavMeshAgent>();

        if (AIPath == null || AIPath.Length < 1)
        {
            ShouldDoPath = false;        
        }
        else
        {
            Agent.speed = (Panicked)?PanickedRunSpeed:WalkSpeed;

            if (_isPatroling)
            {
                StartPatroling();
            }
        }
    }

    private void BecomePanicked()
    {
        Agent.speed = PanickedRunSpeed;
    }

    private void StartPatroling()
    {
        _currentPathPoint = 0;
        Agent.SetDestination(AIPath[_currentPathPoint].position);
        StartCoroutine(PathStep());
    }

    private IEnumerator PathStep()
    {
        yield return new WaitForSeconds(1f);
        while (ShouldDoPath)
        {
            if (Agent.remainingDistance <= Threshold) // if arrived
            {
                if (_currentPathPoint >= AIPath.Length - 1)//finished path
                {
                    if(LoopPatrol)
                    {
                        _currentPathPoint = 0;
                        Agent.SetDestination(AIPath[_currentPathPoint].position);
                    }
                    else
                        ShouldDoPath = false;
                    yield return null;
                }
                else
                {
                    _currentPathPoint = (_currentPathPoint + 1) % AIPath.Length;
                    Agent.SetDestination(AIPath[_currentPathPoint].position);
                    yield return new WaitForSeconds(.1f);
                }
                
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }

    }

    private void Update()
    {
        if (!_shouldUpdate) return;
        if (ShouldDoPath)
        {
            
            _characterController.Move(Agent.velocity.magnitude / Agent.speed);
        }
        else
        {
            _characterController.Move(0f);
            _shouldUpdate = false;
        }
    }
}
