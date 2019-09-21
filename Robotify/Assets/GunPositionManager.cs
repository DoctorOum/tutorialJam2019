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

    private void Start()
    {
        pam = GetComponentInParent<playerAttackManager>();

        SetGunPosition();
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

    public void DropGun()
    {
        GameObject gunToDrop = pam.guns[frontGunIndex];
        pam.guns.RemoveAt(frontGunIndex);
        gunToDrop.transform.SetParent(null);
        gunToDrop.GetComponent<Gun>().isPickup = true;
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

        if (frontGunIndex > pam.guns.Capacity - 1)
            frontGunIndex = 0;
        if (frontGunIndex < 0)
            frontGunIndex = pam.guns.Capacity - 1;
        canRotateGuns = false;
        StartCoroutine("ScrollDelay");
    }

    public void SetGunPosition()
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
}
