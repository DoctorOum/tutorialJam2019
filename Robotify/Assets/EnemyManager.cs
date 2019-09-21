using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health = 10;
    public int sideCount = 4;
    public int speed;

    [Header("Guns and Gun Positions")]
    public GameObject front;
    public GameObject GunOrganizer;
    public GameObject GunContainer;
    public List<GameObject> guns;

    public EnemySpawn spawnManager;
    public AIStrafe ai;

    private void Start()
    {
        spawnManager = GameObject.FindGameObjectWithTag("Player").GetComponent<EnemySpawn>();
       for(int i = 0; i < Mathf.Round(Random.Range(1,sideCount)); i++)
       {
            int randomNum = Mathf.RoundToInt(Random.Range(0f, spawnManager.guns.Count - 1));
            GameObject AddGun = Instantiate(spawnManager.guns[randomNum]);
            guns.Add(AddGun);
            AddGun.transform.parent = GunContainer.transform;
            AddGun.GetComponent<Gun>().fireRateMult = 0.5f;
            AddGun.GetComponent<Gun>().isEnemy = true;
       }
        //print("Gun Capacity : " + guns.Count);
        SetGunPositions();
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

    void Hit(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            Hit(collision.gameObject.GetComponent<Projectile>().damage);
        }
    }
}
