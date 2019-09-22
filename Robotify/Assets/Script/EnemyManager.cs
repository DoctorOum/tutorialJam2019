using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health = 10;
    private float maxHealth = 10;
    public int sideCount = 4;
    public int speed;

    [Header("Guns and Gun Positions")]
    public GameObject front;
    public GameObject GunOrganizer;
    public GameObject GunContainer;
    public List<GameObject> guns;

    public ProgressionManager pm;
    public EnemySpawn spawnManager;
    public AIStrafe ai;

    private void Start()
    {
        pm = GameObject.FindGameObjectWithTag("ProgressionManager").GetComponent<ProgressionManager>();

        maxHealth = 10 + (0.01f *pm.killCount);
        health = maxHealth;

        spawnManager = GameObject.FindGameObjectWithTag("Player").GetComponent<EnemySpawn>();
       for(int i = 0; i < Mathf.Round(Random.Range(1,sideCount)); i++)
       {
            int randomNum = Mathf.RoundToInt(Random.Range(0f, spawnManager.guns.Count - 1));
            GameObject AddGun = Instantiate(spawnManager.guns[randomNum]);
            Gun gunScript = AddGun.GetComponent<Gun>();
            gunScript.damageMult = 1 + (0.01f * pm.killCount);
            //print("Gun Spawn Damage Mult: " + gunScript.damageMult);
            guns.Add(AddGun);
            AddGun.transform.parent = GunContainer.transform;
            gunScript.fireRateMult = 0.5f;
            gunScript.isEnemy = true;
       }
        //print("Gun Capacity : " + guns.Count);
        SetGunPositions();
    }

    private void OnDestroy()
    {
        float trial = Random.Range(0f, 99f);
        if (trial < 20)
        {
            GameObject NewGun = Instantiate(spawnManager.guns[Random.Range(0, spawnManager.guns.Count)], transform.position, Quaternion.identity);
            NewGun.GetComponent<Gun>().Dropped();
            NewGun.GetComponent<Gun>().damageMult = 0.01f * (pm.killCount * Random.Range(1.0f, 2.0f));
            //print("Gun Spawn Damage Mult: " + NewGun.GetComponent<Gun>().damageMult);
        }
    }

    private void Update()
    {
        if (ai.inRange)
        {
            Fire();
        }
        
    }

    private void Fire()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            if(guns[i] != null)
                guns[i].GetComponent<Gun>().Fire();
        }
    }

    public void SetGunPositions()
    {
        float angle = 360f / sideCount;
        for (int i = 0; i < guns.Count; i++)
        {
            if (guns[i] != null)
            {
                guns[i].transform.position = front.transform.position;
                guns[i].transform.rotation = front.transform.rotation;
            }
            GunOrganizer.transform.Rotate(new Vector3(0, 0, -angle));
        }
    }

    public void Hit(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            pm.onKill(maxHealth);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if(collision.gameObject.tag == "PlayerProjectile")
        {
            Hit(collision.gameObject.GetComponent<Projectile>().damage);
        }
    }
}
