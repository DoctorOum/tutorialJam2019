using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    playerAttackManager pam;
    int ammo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        pam = gameObject.GetComponent<playerAttackManager>();
        ammo = (int)(2/* * number of sides*/ * pam.guns.Length * Random.Range(.7f, 1.3f));
        if(collision.tag == "Ammo")
        {
            //when playerAttackManager knows about ammo, add picked up ammo to the reserves in playerAttackManager
        }
    }
}