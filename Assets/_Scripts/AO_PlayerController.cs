using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AO_PlayerController : MonoBehaviour {
	[SerializeField] private Transform _cameraTransform;
	[SerializeField] private float _turnSpeed;
	[SerializeField] private Transform _controlledObject;
	[SerializeField] private AO_CameraObject _cameraObject;

    private bool _isInfected = false;

    private Vector3 _aimPoint;
    public Vector3 AimPoint
    {
        get { return _aimPoint; }
    }

	AO_CharacterController characterController;

    public GameEvent PanicStartedEvent;
    private bool _firstBlood = false;

    public AO_CharacterController CharacterCtrlr
    {
        get { return characterController; }
    }

	Vector3 lerpDirection;
	Vector3 axisVector;
	Vector3 cameraRelative;
	Animator animator;

	bool inBiteRange;
	bool inInteractRange;

    private bool _playerDied = false;

    public bool ReadingText = false;
	
	// Use this for initialization
	void Start () {
		animator = _controlledObject.GetComponent<Animator>();
        _controlledObject.GetComponent<Rigidbody>().isKinematic = false;
        animator.applyRootMotion = true;
        characterController = _controlledObject.GetComponent<AO_CharacterController>();
        ChangeLayer( characterController.gameObject,LayerMask.NameToLayer("Player"));
        //characterController.BecomeInfected();

    }

    public void BecomeInfected()
    {
        _isInfected = true;
        CharacterCtrlr.BecomeInfected();
    }

    private void ChangeLayer(GameObject go, int layer)
    {
        go.layer = layer;
        Transform[] ts = go.GetComponentsInChildren<Transform>();

        foreach (Transform t in ts)
        {
            t.gameObject.layer = layer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerDied) return;
        axisVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        cameraRelative = axisVector.x * _cameraTransform.right + axisVector.z * _cameraTransform.forward;
        cameraRelative.y = 0;

        if (axisVector.magnitude > 0)
        {
            lerpDirection = Vector3.Lerp(lerpDirection, cameraRelative, Time.deltaTime * _turnSpeed);
            characterController.Move(lerpDirection, axisVector);
        }

        if (Input.GetButtonDown("Action"))
        {
            if (characterController.GetClosestVictimInRange() != null && _isInfected) //if have a victom close by, bite !
            {
                //Rotate to the actor. And also character will rotate back.
                if(!_firstBlood)
                {
                    _firstBlood = true;
                    PanicStartedEvent.Raise();
                }
                characterController.DoAction(CharacterAction.Bite);
            }
            else //else, check if any interactable close by, if so, interact!
            {
                IInterractable interactable = characterController.GetClosestInteractable();

                if (interactable != null)
                {
                    interactable.Interract();
                }
            }
        }
        else if (_isInfected && Input.GetButtonDown("Fire1"))
        {

            if (GetMousePositionOverGround(out _aimPoint))
            {
                Debug.LogWarning("AIMING");
                _aimPoint = new Vector3(_aimPoint.x, _aimPoint.y + 1, _aimPoint.z);
                characterController.DoAction(CharacterAction.Spit);
            }
            else
            {
                Debug.LogWarning("Huh missed");
            }
        }
    }

    

	public void ChangeCharacter(Transform newCharacter)//Call this on event "OnCharacterChange"
	{
		_controlledObject = newCharacter;
        animator = _controlledObject.GetComponent<Animator>();
        _controlledObject.GetComponent<Rigidbody>().isKinematic = false;
        animator.applyRootMotion = true;
        characterController = _controlledObject.GetComponent<AO_CharacterController>();
        ChangeLayer(characterController.gameObject, LayerMask.NameToLayer("Player"));
        characterController.BecomeInfected();

        _cameraObject._followTarget = _controlledObject;
	}    

    public bool GetMousePositionOverGround(out Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 300, LayerMask.GetMask("Floor")))
        {
            position = hit.point;
            return true;
        }
        else
        {
            position = Vector3.zero;
            return false;
        }
    }

    public void OnPlayerDied()
    {
        _playerDied = true;
    }
}
