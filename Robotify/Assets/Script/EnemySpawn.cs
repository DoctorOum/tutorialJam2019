using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public int enemyAmount;
    public GameObject enemyToSpawn;
    GameObject parent;
    float spawnDistance = 24f;

    void Start()
    {
        parent = GameObject.Find("Enemy Container");
    }
    
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < enemyAmount)
        {
            bool hasDestroyed;
            do
            {
                Vector3 temp = new Vector3(transform.position.x + Random.Range(-spawnDistance, spawnDistance), transform.position.y + Random.Range(-spawnDistance, spawnDistance), transform.position.z);
                GameObject spawn = Instantiate(enemyToSpawn, temp, Quaternion.Euler(0f, 0f, Random.Range(0, 360)));
                spawn.transform.SetParent(parent.transform);
                if (Mathf.Abs(spawn.transform.position.x - gameObject.transform.position.x) < .5f * spawnDistance && Mathf.Abs(spawn.transform.position.y - gameObject.transform.position.y) < .5f * spawnDistance)
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