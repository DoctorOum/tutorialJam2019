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

    [Header("Dropped State")]
    public bool isPickup;

    public void Dropped()
    {
        StartCoroutine("DropDelay");
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
    }
    IEnumerator FIRE()
    {
        foreach (GameObject spawnPoint in projectileSpawns)
        {
            GameObject shot = Instantiate(projectile, spawnPoint.transform.position, transform.rotation);
            Projectile currentP = shot.GetComponent<Projectile>();

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
