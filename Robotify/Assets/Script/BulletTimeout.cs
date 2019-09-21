using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTimeout : MonoBehaviour
{
    void Start()
    {
        Invoke("endOfLife", 5f);
    }

    void endOfLife()
    {
        Destroy(gameObject);
    }
}