using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AO_RoomTrigger : MonoBehaviour {


    public GameEvent EventToTrigger;
    public AO_CharacterType TypeToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        AO_CharacterHumanController ctrl = other.gameObject.GetComponent<AO_CharacterHumanController>();
        if (ctrl != null && ctrl.CharacterType.Equals(TypeToTrigger))
        {
            EventToTrigger.Raise();
        }
    }
}
