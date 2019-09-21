using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float flightSeed;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * Time.deltaTime * flightSeed);
    }
}
