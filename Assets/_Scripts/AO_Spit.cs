using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AO_Spit : MonoBehaviour {


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION!");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("ENEMY!");
            AO_CharacterController cc =  collision.gameObject.GetComponent<AO_CharacterController>();
            if (cc != null)
            {
                cc.GetHitByProjectile();
            }

            Destroy(gameObject);
        }

        else if (collision.gameObject.layer == LayerMask.GetMask("Floor", "Default"))
        {
            Debug.Log("STATIC STUFF!");
            Destroy(gameObject);
        }
        
    }
}
