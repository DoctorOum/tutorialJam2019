using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    GameObject Player;
    public float distanceFromPlayer = 15f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Player.transform.position);

        if (hit.collider.gameObject.tag == "Player")
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, hit.collider.transform.position, distanceFromPlayer);
        }


    }
}
