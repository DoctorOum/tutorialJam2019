using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3SmoothDamp : MonoBehaviour
{
    GameObject target;
    int playerSize;
    float dampTime = .1f;
    Vector3 velocity = Vector3.zero;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        playerSize = target.GetComponent<playerAttackManager>().sideCount;
    }
    private void Update()
    {
        if (target)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + new Vector3(0f, 0f, -5f), ref velocity, dampTime);
        }
        if(playerSize != target.GetComponent<playerAttackManager>().sideCount)
        {
            playerSize = target.GetComponent<playerAttackManager>().sideCount;
            gameObject.GetComponent<Camera>().orthographicSize = 2 + playerSize;
        }
    }
}