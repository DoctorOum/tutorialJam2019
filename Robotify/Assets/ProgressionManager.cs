using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public int killCount = 0;
    public int score = 0;
    public float playerHealth = 100;

    public GunPositionManager gpm;

    public void onKill(float maxHealth)
    {
        killCount++;
        playerHealth += maxHealth * 0.5f;
        if(playerHealth >= 200)
        {
            gpm.AlterSideCount(1);
        }
    }

    public void Hit(float damage)
    {
        playerHealth -= damage;
        if(playerHealth <= 0)
        {
            print("Player Death");
        }
    }
     
}
