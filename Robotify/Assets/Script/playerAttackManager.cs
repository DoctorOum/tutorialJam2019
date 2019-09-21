using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackManager : MonoBehaviour
{

    public GameObject[] guns;

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            FireGuns();
        }
    }

    private void FireGuns()
    {
        foreach(GameObject gun in guns)
        {
            Gun gunScript = gun.GetComponent<Gun>();
            gunScript.Fire();
            //print("Called Fire Function");
        }
    }
}
