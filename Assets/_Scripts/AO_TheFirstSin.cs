using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AO_TheFirstSin : MonoBehaviour, IInterractable
{
    public AO_PlayerController _playerController;

    public GameEvent InfectionEvent;

    private void Awake()
    {
        if (_playerController == null)
        {
            _playerController = FindObjectOfType<AO_PlayerController>();
        }
    }

    public void Interract()
    {
        InfectionEvent.Raise();
    }

    private void OnTriggerEnter(Collider other)
    {
        AO_CharacterHumanController ctrl = other.gameObject.GetComponent<AO_CharacterHumanController>();
        if (ctrl != null && ctrl.Equals(_playerController.CharacterCtrlr))
        {
            ctrl.AddUniqueInteractable(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AO_CharacterHumanController ctrl = other.gameObject.GetComponent<AO_CharacterHumanController>();
        if (ctrl != null && ctrl.Equals(_playerController.CharacterCtrlr))
        {
            _playerController.CharacterCtrlr.interractables.Remove(this);
        }
    }
}
