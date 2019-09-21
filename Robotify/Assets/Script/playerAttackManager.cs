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
            gpm.SetGunPositions();
        }

    }

    private void FireGuns()
    {
        for(int i = 0; i < guns.Count; i++)
        {
            if (guns[i] != null)
            {
                Gun gunScript = guns[i].GetComponent<Gun>();
                gunScript.Fire();
                //print("Called Fire Function");
            }
        }
    }
}
