using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public int ammoAmount;
    public GameObject ammoToSpawn;
    GameObject parent;
    float spawnDistance = 60f;

    void Start()
    {
        parent = GameObject.Find("Ammo Container");
    }
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Ammo").Length < ammoAmount)
        {
            bool hasDestroyed;
            do
            {
                Vector3 temp = new Vector3(transform.position.x + Random.Range(-spawnDistance, spawnDistance), transform.position.y + Random.Range(-spawnDistance, spawnDistance), transform.position.z);
                GameObject spawn = Instantiate(ammoToSpawn, temp, Quaternion.Euler(0f, 0f, Random.Range(0, 360)));
                spawn.transform.SetParent(parent.transform);
                if (Mathf.Abs(spawn.transform.position.x - gameObject.transform.position.x) < .3f * spawnDistance && Mathf.Abs(spawn.transform.position.y - gameObject.transform.position.y) < .3f * spawnDistance)
                {
                    Destroy(spawn);
                    hasDestroyed = true;
                }
                else
                {
                    spawn.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    hasDestroyed = false;
                }
            } while (hasDestroyed);
        }
    }
}