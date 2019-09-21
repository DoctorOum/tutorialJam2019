using System.Collections;
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

    public void AddSide()
    {
        pam.sideCount++;
        pam.guns.Add(null);
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
    }

    public void PickUpGun(GameObject gunToPickup)
    {
        if (canPickupGuns && gunToPickup.GetComponentInParent<Gun>().isPickup)
        {
            print("Front Gun Index " + frontGunIndex + " is " + pam.guns[frontGunIndex]);
            if (pam.guns[frontGunIndex] == null)
            {
                pam.guns[frontGunIndex] = gunToPickup.transform.parent.gameObject;
                print("Front Gun Index " + frontGunIndex + " is now " + pam.guns[frontGunIndex]);
                gunToPickup.transform.parent.SetParent(gunContainer.transform);
                gunToPickup.transform.parent.transform.position = front.transform.position;
                gunToPickup.transform.parent.transform.rotation = front.transform.rotation;
                gunToPickup.GetComponentInParent<Gun>().isPickup = false;
            }
            else
            {
                print("Error: Front full");
            }
        }
    }

    public void DropGun()
    {
        if(pam.guns[frontGunIndex] != null)
        {
            GameObject gunToDrop = pam.guns[frontGunIndex];
            pam.guns[frontGunIndex] = null;
            gunToDrop.transform.SetParent(null);
            gunToDrop.GetComponent<Gun>().Dropped();
            StartCoroutine("PickupDelay");
        }
    }

    public void RotateGuns(float direction)
    {
        
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
        StartCoroutine("ScrollDelay");
    }

    public void SetGunPositions()
    {
        frontGunIndex = 0;
        float angle = 360f / pam.sideCount;
        for(int i = 0; i < pam.guns.Capacity; i++)
        {
            pam.guns[i].transform.position = front.transform.position;
            pam.guns[i].transform.rotation = front.transform.rotation;

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
        print("Collided with: " + collision.gameObject.name);
        if (collision.tag == "Gun")
        {
            PickUpGun(collision.gameObject);
        }
    }
}
