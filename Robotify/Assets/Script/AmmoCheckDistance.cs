using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCheckDistance : MonoBehaviour
{
    GameObject player;
    int spawnDistance = 65;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) > spawnDistance || Mathf.Abs(player.transform.position.y - gameObject.transform.position.y) > spawnDistance)
        {
            Destroy(gameObject);
        }
    }
}