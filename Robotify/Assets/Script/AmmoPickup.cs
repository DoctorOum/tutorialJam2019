using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    playerAttackManager pam;
    GameObject player;
    int ammo;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pam = player.GetComponent<playerAttackManager>();        
        ammo = (int)(pam.sideCount * pam.guns.Length * Random.Range(.7f, 1.3f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            pam.ammo += ammo;
            Destroy(gameObject);
        }
    }
}