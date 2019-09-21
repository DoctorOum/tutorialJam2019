using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPositionManager : MonoBehaviour
{
    private playerAttackManager pam;
    [SerializeField] private GameObject front;

    private void Start()
    {
        pam = GetComponentInParent<playerAttackManager>();
    }

    public void UpdateGunPositions()
    {
        float angle = 360f / pam.sideCount;
        for(int i = 0; i < pam.guns.Length; i++)
        {
            pam.guns[i].transform.position = front.transform.position;
            pam.guns[i].transform.rotation = front.transform.rotation;

            transform.Rotate(new Vector3(0, 0, angle));
        }

        print("Updated Guns");
    }
}
