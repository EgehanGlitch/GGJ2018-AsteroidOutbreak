using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AO_OutbreakManager : MonoBehaviour {

    private AO_TalkableNPC[] talkableNPCs;

    // Use this for initialization
    void Start () {
        talkableNPCs = FindObjectsOfType<AO_TalkableNPC>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartOutbreak()
    {
        for (int i = talkableNPCs.Length-1; i >0 ; i--)
        {
            Destroy(talkableNPCs[i]);
        }
        talkableNPCs = null;


        FindObjectOfType<AO_PlayerController>().BecomeInfected();
    }

    public void TriggerPanic()
    {
        AO_BasicAI[] ais = FindObjectsOfType<AO_BasicAI>();
        for (int i = 0; i < ais.Length; i++)
        {
            ais[i].Panicked = true;
        }
    }
}
