using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionManager : MonoBehaviour
{
    [Header("Scoring")]
    public int killCount = 0;
    public int score = 0;
    [Header("Player Health and Scaling")]
    public float playerHealth = 100;
    public float lastGoal = 100;
    public float upgradeThreshold = 100;
    public float lifeStealPerKill = 0.5f;

    [Header("Required Refrences")]
    public GunPositionManager gpm;
    public GameObject InGameUI;
    public GameObject deathScreen;
    public EnemySpawn enemySpawner;

    [Header("UI Stuff")]
    public Text healthText;

    private void Start()
    {
        healthText.text = playerHealth.ToString();
        AlterEnemyCount();
    }

    private void AlterEnemyCount()
    {
        
        if (playerHealth > 100)
        {
            enemySpawner.enemyAmount = (int)(3 + (playerHealth / 100));
        }
        else if (playerHealth < 100)
        {
            enemySpawner.enemyAmount = 3;
        }

    }

    public void onKill(float maxHealth)
    {
        killCount++;
        score += 100;
        playerHealth += maxHealth * lifeStealPerKill;
        if(playerHealth >= lastGoal + upgradeThreshold)
        {
            lastGoal += upgradeThreshold;
            gpm.AlterSideCount(1);
        }
        if(playerHealth <= lastGoal)
        {
            lastGoal -= upgradeThreshold;
            gpm.AlterSideCount(-1);
        }
        healthText.text = playerHealth.ToString();
        AlterEnemyCount();
    }

    public void Hit(float damage)
    {
        playerHealth -= damage;
        healthText.text = playerHealth.ToString();
        if(playerHealth <= 0)
        {
            print("Player Death");
            deathScreen.SetActive(true);

        }
        if (playerHealth <= lastGoal)
        {
            while(playerHealth <= lastGoal)
            {
                lastGoal -= upgradeThreshold;
                gpm.AlterSideCount(-1);
            }
            
        }
    }
     
}
