using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    GameObject player;
    float distance = 31f;
    float length = 60f;
    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        if (player.transform.position.y - distance > gameObject.transform.position.y)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + length, gameObject.transform.position.z);
        }
        else if (player.transform.position.y + distance < gameObject.transform.position.y)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - length, gameObject.transform.position.z);
        }

        if (player.transform.position.x - distance > gameObject.transform.position.x)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + length, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (player.transform.position.x + distance < gameObject.transform.position.x)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - length, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
}