using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AO_LockedDoor : MonoBehaviour, IInterractable
{

    public AO_CharacterType CharacterForRetinaScan;

    private AO_PlayerController _playerController;

    public Transform DoorPart;

    public bool IsOpen = false;

    private Animator _animator;

    private void Start()
    {
        _playerController = FindObjectOfType<AO_PlayerController>();
        if (_playerController == null)
        {
            Debug.LogError("COULDNT FIND THE PLAYER CONTROLLER");

        }
    }

    public void Interract()
    {
        
        if (IsOpen) return;
        if (_playerController.CharacterCtrlr is AO_CharacterHumanController) 
        {
            Debug.LogWarning("INTERRACTING!!!!!");
            if ((_playerController.CharacterCtrlr as AO_CharacterHumanController).CharacterType.Equals( CharacterForRetinaScan))
            {
                Debug.LogWarning("OPENING!");
                OpenDoor();
            }
            else // rejected
            {

            }
        }
        else //rejected
        {

        }
    }

    private void OpenDoor()
    {
        if (IsOpen) return;
        _animator.SetTrigger("OpenDoor");
        IsOpen = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        AO_CharacterHumanController ctrl = other.gameObject.GetComponent<AO_CharacterHumanController>();
        if (ctrl != null && ctrl.Equals( _playerController.CharacterCtrlr))
        {
            ctrl.AddUniqueInteractable(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        AO_CharacterHumanController ctrl = other.gameObject.GetComponent<AO_CharacterHumanController>();
        if (ctrl != null && ctrl.Equals( _playerController.CharacterCtrlr))
        {
            _playerController.CharacterCtrlr.interractables.Remove(this);
        }
    }
}
