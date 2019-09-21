using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackManager : MonoBehaviour
{
    public int ammo;
    public int sideCount = 4;
    public GunPositionManager gpm;
    public List<GameObject> guns;

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            FireGuns();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            gpm.SetGunPosition();
        }

    }

    private void FireGuns()
    {
        foreach (GameObject gun in guns)
        {
            Gun gunScript = gun.GetComponent<Gun>();
            gunScript.Fire();
            //print("Called Fire Function");
        }
    }
}
