using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField]private float damage = 5;
    [SerializeField]private float fireRate = 1;
    [SerializeField]private float projectileSpeed = 10;
    public float damageMult = 1;
    public float fireRateMult = 1;
    public bool SpreadDamageOverBullets;

    [Header("Projectiles and Spawnpoints")]
    [SerializeField]private GameObject[] projectileSpawns;
    [SerializeField]private GameObject projectile;
    public bool AlternateBarrels;
    private bool firing;

    [Header("States")]
    public bool isEnemy;
    public bool isPickup;
    private int decayTime = 10;

    private void Start()
    {
        if (!isPickup)
        {
            GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
    }

    public void PickedUp()
    {
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        isPickup = false;
    }

    public void Dropped()
    {
        isEnemy = false;
        isPickup = true;
        StartCoroutine("DropDelay");
        StartCoroutine("DecayTimer");
    }

    public void Fire()
    {
        if (!firing)
        {
            firing = true;
            StartCoroutine("FIRE");
        }
        
    }

    IEnumerator DropDelay()
    {
        yield return new WaitForSeconds(1f);
        isPickup = true;
        GetComponentInChildren<BoxCollider2D>().enabled = true;
    }
    IEnumerator DecayTimer()
    {
        //print("isPickup: " + isPickup);
        for(int i = decayTime; i >= 0; i--)
        {
            if (!isPickup)
            {
                //print("picked Up");
                break;
            }
            yield return new WaitForSeconds(1f);
        }
        if (isPickup)
        {
            //print("Killed it");
            Destroy(gameObject);
        }
        

    }
    IEnumerator FIRE()
    {
        foreach (GameObject spawnPoint in projectileSpawns)
        {
            GameObject shot = Instantiate(projectile, spawnPoint.transform.position, transform.rotation);
            Projectile currentP = shot.GetComponent<Projectile>();
            if (isEnemy)
            {
                shot.tag = "EnemyProjectile";
                shot.layer = 9;
            }
            else
            {
                shot.tag = "PlayerProjectile";
                shot.layer = 10;
            }
            
            if (SpreadDamageOverBullets)
            {
                currentP.damage = (damage * damageMult)/projectileSpawns.Length;
            }
            else
            {
                currentP.damage = damage * damageMult;
            }

            currentP.flightSeed = projectileSpeed;

            if (AlternateBarrels)
                yield return new WaitForSeconds((fireRate / fireRateMult)/projectileSpawns.Length);
        }
        
        //print("Fired Gun");
        if (!AlternateBarrels)
            yield return new WaitForSeconds((fireRate / fireRateMult));
        firing = false;
    }
}
