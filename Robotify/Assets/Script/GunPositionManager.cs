﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPositionManager : MonoBehaviour
{
    public int frontGunIndex = 0;
    private playerAttackManager pam;
    [SerializeField] private GameObject front;
    [SerializeField] private GameObject gunContainer;
    private bool canRotateGuns = true;
    private bool canPickupGuns = true;

    private void Start()
    {
        pam = GetComponentInParent<playerAttackManager>();
        SetGunPositions();
    }

    public void AlterSideCount(int amount)
    {
        pam.sideCount += amount;
        if(amount > 0)
        {
            for (int i = amount; i > 0; i--)
                pam.guns.Add(null);
        }
        else
        {
            for (int i = amount; i < 0; i++)
                if(pam.guns[pam.guns.Count - 1] != null)
                    DropGun(true);
                pam.guns.RemoveAt(pam.guns.Count - 1);
        }
        SetGunPositions();
    }

    private void Update()
    {
        if(Input.GetAxisRaw("Mouse ScrollWheel") != 0 && canRotateGuns)
        {
            RotateGuns(Input.GetAxisRaw("Mouse ScrollWheel"));
            
        }
        if (Input.GetButtonDown("DropGun"))
        {
            DropGun();
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            AlterSideCount(1);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            AlterSideCount(-1);
        }
    }

    public void PickUpGun(GameObject gunToPickup)
    {
        if (canPickupGuns && gunToPickup.GetComponentInParent<Gun>().isPickup)
        {
            //print("Front Gun Index " + frontGunIndex + " is " + pam.guns[frontGunIndex]);
            if (pam.guns[frontGunIndex] == null)
            {
                pam.guns[frontGunIndex] = gunToPickup.transform.parent.gameObject;
                gunToPickup.GetComponent<SpriteRenderer>().color = Color.yellow;
                print("Front Gun Index " + frontGunIndex + " is now " + pam.guns[frontGunIndex]);

                gunToPickup.transform.parent.SetParent(gunContainer.transform);
                gunToPickup.transform.parent.transform.position = front.transform.position;
                gunToPickup.transform.parent.transform.rotation = front.transform.rotation;

                gunToPickup.GetComponentInParent<Gun>().pam = pam;
                gunToPickup.GetComponentInParent<Gun>().PickedUp();
            }
            else
            {
                print("Error: Front full");
            }
        }
    }

    public void DropGun(bool removeSide = false)
    {
        if (!removeSide)
        {
            if (pam.guns[frontGunIndex] != null)
            {
                GameObject gunToDrop = pam.guns[frontGunIndex];
                pam.guns[frontGunIndex] = null;
                gunToDrop.transform.SetParent(null);
                gunToDrop.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                gunToDrop.GetComponent<Gun>().Dropped();
                StartCoroutine("PickupDelay");
            }
        }
        else
        {
            GameObject gunToDrop = pam.guns[pam.guns.Count - 1];
            pam.guns[pam.guns.Count - 1] = null;
            gunToDrop.transform.SetParent(null);
            gunToDrop.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            gunToDrop.GetComponent<Gun>().Dropped();
            StartCoroutine("PickupDelay");
        }
        
    }

    public void RotateGuns(float direction)
    {
        if (pam.guns[frontGunIndex] != null)
            pam.guns[frontGunIndex].GetComponentInChildren<SpriteRenderer>().color = Color.white;

        float angle = 360f / pam.sideCount;
        if(direction > 0)
        {
            gunContainer.transform.Rotate(new Vector3(0, 0, angle));
            frontGunIndex++;
        }
        else if(direction < 0)
        {
            gunContainer.transform.Rotate(new Vector3(0, 0, -angle));
            frontGunIndex--;
        }

        if (frontGunIndex > pam.sideCount - 1)
            frontGunIndex = 0;
        if (frontGunIndex < 0)
            frontGunIndex = pam.sideCount - 1;
        canRotateGuns = false;
        if(pam.guns[frontGunIndex] != null)
        {
            pam.guns[frontGunIndex].GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
        }
        StartCoroutine("ScrollDelay");
    }

    public void SetGunPositions()
    {
        frontGunIndex = 0;
        float angle = 360f / pam.sideCount;
        for(int i = 0; i < pam.guns.Capacity; i++)
        {
            if(pam.guns[i] != null)
            {
                pam.guns[i].transform.position = front.transform.position;
                pam.guns[i].transform.rotation = front.transform.rotation;
            }
            

            transform.Rotate(new Vector3(0, 0, -angle));
        }

        print("Updated Guns");
    }

    IEnumerator ScrollDelay()
    {
        yield return new WaitForSeconds(0.1f);
        canRotateGuns = true;
    }
    IEnumerator PickupDelay()
    {
        yield return new WaitForSeconds(0.5f);
        canPickupGuns = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Gun")
        {
            PickUpGun(collision.gameObject);
        }
    }
}