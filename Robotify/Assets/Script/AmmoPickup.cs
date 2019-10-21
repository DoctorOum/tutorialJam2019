using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    playerAttackManager pam;
    GameObject player;
    public int ammo;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pam = player.GetComponent<playerAttackManager>();        
        ammo = (int)((pam.guns.Count * 10));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            pam.AlterAmmo(ammo);
            Destroy(gameObject);
        }
    }
}