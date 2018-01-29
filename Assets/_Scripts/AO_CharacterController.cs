using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CharacterAction
{
	Idle,
	Hide,
	Bite,
	Lunge,
	Throw,
	Interact,
	Death,
    Spit
}

public abstract class AO_CharacterController : MonoBehaviour {
	

	Animator animator;
	CharacterAction currentAction = CharacterAction.Idle;
	Vector3 axisVector;
	Vector3 cameraRelative;

	Vector3 lerpDirection;

	bool isOnAction;

    public List<Transform> infectablesInRange = new List<Transform>();
    public List<IInterractable> interractables = new List<IInterractable>();

    [HideInInspector]
	public bool inBiteRange;

	protected Transform selectedInfectable;

    public bool IsInfected = false;
    public bool IsDead = false;


    public GameEvent EventPlayerDied;

    public float TimeItTakesToDieFromInfection = 10f;
    private float _timeInfectionStarted;
    private bool _timedOut = false;


    void Start () {
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if(animator.GetBool("DyingAction"))
		{
			
			
		}
	}

    public IInterractable GetClosestInteractable()
    {
        if(interractables.Count < 1)
        {
            return null;
        }
        return interractables[interractables.Count - 1];
    }

    public void AddUniqueInteractable(IInterractable interactable)
    {
        if (!interractables.Contains(interactable))
        {
            interractables.Add(interactable);
        }
    }

    public void BecomeInfected()
    {
        IsInfected = true;
        _timeInfectionStarted = Time.time;
        _timedOut = false;

        StartCoroutine(InfectionTick());
    }
	
	// Update is called once per frame
	public void Move(float speed) 
	{		
		if(currentAction == CharacterAction.Idle)
		{
			//transform.rotation = Quaternion.LookRotation(direction);

			//float axisSpeed = Mathf.Max(Mathf.Abs(speed.normalized.x), Mathf.Abs(speed.normalized.z));
			animator.SetFloat("ForwardSpeed", speed);
		}

	}
	public void Move(Vector3 direction, Vector3 axisVector)
	{
        if (animator.GetBool("OnAction"))
        {
            return;
        }

		transform.rotation = Quaternion.LookRotation(direction);

		float axisSpeed = Mathf.Max(Mathf.Abs(axisVector.x), Mathf.Abs(axisVector.z));
		animator.SetFloat("ForwardSpeed", axisSpeed);
	}

	public virtual void DoAction(CharacterAction charAction)
	{

        switch (charAction)
        {
            case CharacterAction.Bite:
                Bite();
                break;
            case CharacterAction.Interact:
                animator.SetTrigger("Interact");
                break;
            case CharacterAction.Lunge:
                animator.SetTrigger("Lunge");
                break;
            case CharacterAction.Hide:
                animator.SetTrigger("Hide");
                break;
            case CharacterAction.Throw:
                animator.SetTrigger("Throw");
                break;
            case CharacterAction.Death:
                animator.SetTrigger("Death");
                break;
            case CharacterAction.Spit:
                //todo animation
                transform.LookAt(FindObjectOfType<AO_PlayerController>().AimPoint,Vector3.up);
                break;
        }
	}

	void OnTriggerEnter(Collider other)
	{
        if (!IsInfected) return;
		if(other.transform.tag == "Infectable" && !other.GetComponent<AO_CharacterController>().IsDead)
		{
			infectablesInRange.Add(other.transform);
		}

	}

	void OnTriggerExit(Collider other)
	{
        if (!IsInfected) return;
        if (other.transform.tag == "Infectable")
		{

            infectablesInRange.Remove(other.transform);
		}

	}

    public Transform GetClosestVictimInRange()
    {
        Transform found = null;

        if (infectablesInRange.Count > 0)
        {
            float distance = 10;
            foreach (Transform t in infectablesInRange)
            {
                if (distance > Vector3.Distance(transform.position, t.position))
                {
                    distance = Vector3.Distance(transform.position, t.position);
                    found = t;
                }
            }
        }

        return found;
    }
	void Bite()
	{
        selectedInfectable = GetClosestVictimInRange();
        if (selectedInfectable == null) return;
		selectedInfectable.LookAt(transform);
        StopCharacter(selectedInfectable);

        transform.LookAt(selectedInfectable);

		animator.SetTrigger("Bite");
		selectedInfectable.GetComponent<Animator>().SetTrigger("Bitten");
		currentAction = CharacterAction.Bite;

	}

    public static void StopCharacter(Transform character)
    {
        Destroy(character.GetComponent<NavMeshAgent>());
        Destroy(character.GetComponent<AO_BasicAI>());
        Rigidbody r = character.GetComponent<Rigidbody>();
        r.isKinematic = true;
        r.velocity = Vector3.zero;
        Animator anim = character.GetComponent<Animator>();
        anim.applyRootMotion = false;
        anim.SetFloat("ForwardSpeed", 0f);
        
    }

	public virtual void TriggerDeath()
	{
        Die();

        AO_PlayerController p = GameObject.FindWithTag("Player").GetComponent<AO_PlayerController>();
        //Add this character to dead list
        //If important also check off the important character list.


        if (selectedInfectable != null) p.ChangeCharacter(selectedInfectable.transform);
        
    }

    private void Die()
    {
        IsDead = true;
        animator.SetTrigger("Death");

        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<CapsuleCollider>());
    }

    public void GetHitByProjectile()
    {
        //todo stop what you doing
        //todo do anim
        TriggerDeath();
    }

    public void FootTouch()
	{

	}

    private IEnumerator InfectionTick()
    {
        while(!_timedOut)
        {
            if (TimeItTakesToDieFromInfection + _timeInfectionStarted < Time.time)
            {
                Debug.Log("TIMED OUT!");
                _timedOut = true;
                if (IsDead)
                {
                    Debug.Log("Was Already dead");
                }
                else
                {
                    
                    if (IsInfected)
                    {
                        EventPlayerDied.Raise();
                    }
                    Die();
                    Debug.Log("KILLED!");
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
