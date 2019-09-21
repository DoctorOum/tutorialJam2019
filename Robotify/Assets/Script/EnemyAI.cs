using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    GameObject Player;
    public float distanceFromPlayer = 15f;
    public float trackingSpeed =20f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Player.transform.position, transform.position);

        if (hit.collider.gameObject.tag == "Player")
        {
            transform.position = Vector2.Lerp(transform.position, Player.transform.position, trackingSpeed * Time.deltaTime);
            transform.up = -(Player.transform.position - transform.position);
        }
    }
}
