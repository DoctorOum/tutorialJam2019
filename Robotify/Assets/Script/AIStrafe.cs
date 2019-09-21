using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStrafe : MonoBehaviour
{
    GameObject Player;
    float sightDistance = 15f;
    float strafeDistance;
    public LayerMask PlayerLayer;
    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        strafeDistance = Mathf.Round(Random.Range(4f, 6f));
    }
    
    void Update()
    {
        Vector2 displacement = new Vector2(Mathf.Abs(Player.transform.position.x - gameObject.transform.position.x), Mathf.Abs(Player.transform.position.y - gameObject.transform.position.y));
        if (Mathf.Sqrt(displacement.x * displacement.x + displacement.y * displacement.y) < sightDistance)
        {
            float angle;
            Vector3 relative = transform.InverseTransformPoint(Player.transform.position);
            angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
            transform.Rotate(0, 0, -angle);
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, PlayerLayer);
            
            if (hit.collider.gameObject.tag == "Player")
            {
                
                if (Mathf.Sqrt(displacement.x * displacement.x + displacement.y * displacement.y) > strafeDistance)
                {
                    gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, hit.collider.transform.position, .05f);
                }
                else if(Mathf.Sqrt(displacement.x * displacement.x + displacement.y * displacement.y) == strafeDistance || strafeDistance - 1 < Mathf.Sqrt(displacement.x * displacement.x + displacement.y * displacement.y))
                {
                    /*
                    gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, new Vector3(5 * Mathf.Cos(Mathf.Rad2Deg * (angle - Mathf.PI / 2)) + gameObject.transform.position.x, 5 * Mathf.Sin(Mathf.Rad2Deg * (angle - Mathf.PI / 2)) + gameObject.transform.position.y, 0), .05f);
                    Debug.Log(new Vector3(5 * Mathf.Cos(Mathf.Rad2Deg * (angle - Mathf.PI / 2)) + gameObject.transform.position.x, 5 * Mathf.Sin(Mathf.Rad2Deg * (angle - Mathf.PI / 2)) + gameObject.transform.position.y, 0));
                    Debug.Log("Theta: " + (Mathf.Rad2Deg * (angle - Mathf.PI / 2)));
                    *///It works... with a few grains of salt D;

                    transform.Translate(Vector3.right * Time.deltaTime * 2);
                }
                else
                {
                    transform.Translate(Vector3.down * Time.deltaTime);
                }
            }
        }
    }
}
