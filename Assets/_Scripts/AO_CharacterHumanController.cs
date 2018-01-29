using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AO_CharacterHumanController: AO_CharacterController {


    public AO_CharacterType CharacterType;
    public Transform SpitStartingPos;
    public Vector3 SpitOffset;

    public override void DoAction(CharacterAction charAction)
    {
        base.DoAction(charAction);

        switch (charAction)
        {
            case CharacterAction.Idle:
                break;
            case CharacterAction.Hide:
                break;
            case CharacterAction.Bite:
                break;
            case CharacterAction.Lunge:
                break;
            case CharacterAction.Throw:
                break;
            case CharacterAction.Interact:
                break;
            case CharacterAction.Death:
                break;
            case CharacterAction.Spit:
                GameObject go = Instantiate(CharacterType.SpitPrefab, SpitStartingPos.position, SpitStartingPos.rotation);
                go.GetComponent<Rigidbody>().AddForce(CharacterType.SpitData.ExplosionForce * (SpitStartingPos.forward + SpitOffset),ForceMode.Impulse);

                break;
            default:
                break;
        }

    }

    public override void TriggerDeath()
    {
        if (selectedInfectable != null)
        {
            NavMeshAgent agent = selectedInfectable.GetComponent<NavMeshAgent>();
            if (agent != null) Destroy(agent);

            AO_BasicAI basicAI = selectedInfectable.GetComponent<AO_BasicAI>();

            if (basicAI != null) Destroy(basicAI);
        }
        base.TriggerDeath();
        CharacterType.OnDeath();        
    }
        
    
}
