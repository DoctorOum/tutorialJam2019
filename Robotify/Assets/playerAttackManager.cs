using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackManager : MonoBehaviour
{

    public GameObject[] guns;


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    private void Fire()
    {

    }
}
