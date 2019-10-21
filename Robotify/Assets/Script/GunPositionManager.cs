using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPositionManager : MonoBehaviour
{
    public int frontGunIndex = 0;
    private playerAttackManager pam;
    [SerializeField] private GameObject front;
    [SerializeField] private GameObject gunContainer;
    private bool canRotateGuns = true;
    private bool canPickupGuns = true;

    [Header("Sprite Renderer")]
    public SpriteRenderer sr;
    public GameObject[] sprites;

    [Header("UI Stuff")]
    public Text gunPowerText;

    [Header("Sound Stuff")]
    public AudioSource audio;
    public AudioClip pickupGunSound;
    public AudioClip dropGunSound;
    public AudioClip gainSideSound;
    public AudioClip loseSideSound;
    public AudioClip gunLossSound;
    public AudioClip rotate;


    private void Start()
    {
        pam = GetComponentInParent<playerAttackManager>();
        SetGunPositions();
        for(int i = 1; i < sprites.Length - 1; i++)
        {
            sprites[i].SetActive(false);
        }
        //print("Gun list capacity: " + pam.guns.Capacity);
    }

    public void UpdateUIElements()
    {
        if (pam.guns[frontGunIndex] == null)
            gunPowerText.text = "";
        else
            gunPowerText.text = pam.guns[frontGunIndex].GetComponent<Gun>().power.ToString();
    }

    public void AlterSideCount(int amount)
    {
        if((pam.sideCount + amount) > 2)
        {
            pam.sideCount += amount;
            int current = pam.sideCount - 3;
            //print("NewSides: " + pam.guns.Capacity);
            if (amount > 0)
            {
                audio.PlayOneShot(gainSideSound);   
                front = sprites[current].GetComponentInChildren<FrontRefrence>().front;
                if ((current - 1) >= 0)
                {
                    sprites[current - 1].SetActive(false);
                }
                sprites[current].SetActive(true);

                for (int i = amount; i > 0; i--)
                    pam.guns.Add(null);
            }
            else
            {
                audio.PlayOneShot(loseSideSound);
                front = sprites[current].GetComponentInChildren<FrontRefrence>().front;
                if ((current + 1) >= 0)
                {
                    sprites[current + 1].SetActive(false);
                }
                sprites[current].SetActive(true);

                for (int i = amount; i < 0; i++)
                {
                    //if (pam.guns[pam.guns.Count - 1] != null)
                        DropGun(true);
                }

            }
            //print("Capacity of list: " + pam.guns.Capacity);
            //print("Made it to end");
            SetGunPositions();
            //RotateGuns(1f);
            //RotateGuns(-1f);
            UpdateUIElements();
        }
        
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
                audio.PlayOneShot(pickupGunSound);
                pam.guns[frontGunIndex] = gunToPickup.transform.parent.gameObject;
                gunToPickup.GetComponent<SpriteRenderer>().color = Color.yellow;
                //print("Front Gun Index " + frontGunIndex + " is now " + pam.guns[frontGunIndex]);

                gunToPickup.transform.parent.SetParent(gunContainer.transform);
                gunToPickup.transform.parent.transform.position = front.transform.position;
                gunToPickup.transform.parent.transform.rotation = front.transform.rotation;

                gunToPickup.GetComponentInParent<Gun>().pam = pam;
                gunToPickup.GetComponentInParent<Gun>().PickedUp();
                UpdateUIElements();
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
                audio.PlayOneShot(dropGunSound);
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
            GameObject gunToDrop;
            int slotToRemove = 0;
            audio.PlayOneShot(gunLossSound);
            pam.AlterAmmo(-(pam.ammo / pam.sideCount));
            //print("Front Gun Index = " + frontGunIndex);
            //prevents the player from losing their currently equipped gun
            if (frontGunIndex == pam.guns.Count - 1)
            {
                if(pam.guns[0] != null)
                {
                    gunToDrop = pam.guns[0];
                    pam.guns[0] = null;
                    //print("pam.guns.count = " + pam.guns.Count);
                }
                else
                {
                    gunToDrop = null;
                }
                slotToRemove = 0;
            }
            else
            {
                gunToDrop = pam.guns[pam.guns.Count - 1];
                pam.guns[pam.guns.Count - 1] = null;
                print("pam.guns.count = " + pam.guns.Count);
                slotToRemove = pam.guns.Count - 1;
                
            }
            
            if(gunToDrop != null)
            {
                gunToDrop.transform.SetParent(null);
                gunToDrop.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                gunToDrop.GetComponent<Gun>().Dropped();
                StartCoroutine("PickupDelay");
            }
            

            pam.guns.RemoveAt(slotToRemove);
            if(frontGunIndex > pam.guns.Count - 1)
            {
                frontGunIndex = pam.guns.Count - 1;
            }
            
        }
        UpdateUIElements();
    }

    public void RotateGuns(float direction, bool sideChange = false)
    {
        /*if (sideChange)
        {
            //gunContainer.transform.rotation = new Quaternion(0, 0, 0, 0);
            print("rotation " + gunContainer.transform.rotation);
            for(int i = frontGunIndex; i > 0; i--)
            {
                if (pam.guns[frontGunIndex] != null)
                    pam.guns[frontGunIndex].GetComponentInChildren<SpriteRenderer>().color = Color.white;

                float angle = 360f / pam.sideCount;
                if (direction > 0)
                {
                    gunContainer.transform.Rotate(new Vector3(0, 0, angle));
                    frontGunIndex++;
                }
                else if (direction < 0)
                {
                    gunContainer.transform.Rotate(new Vector3(0, 0, -angle));
                    frontGunIndex--;
                }

                if (frontGunIndex > pam.sideCount - 1)
                    frontGunIndex = 0;
                if (frontGunIndex < 0)
                    frontGunIndex = pam.sideCount - 1;
                canRotateGuns = false;
                if (pam.guns[frontGunIndex] != null)
                {
                    pam.guns[frontGunIndex].GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
                }
            }
        }*/
        //Rotate Guns but alter array position rather than tracking a front gun
        if(true)
        {
            audio.PlayOneShot(rotate);

            if (pam.guns[frontGunIndex] != null)
                pam.guns[frontGunIndex].GetComponentInChildren<SpriteRenderer>().color = Color.white;

            float angle = 360f / pam.sideCount;
            if (direction > 0)
            {
                gunContainer.transform.Rotate(new Vector3(0, 0, angle));
                frontGunIndex++;
            }
            else if (direction < 0)
            {
                gunContainer.transform.Rotate(new Vector3(0, 0, -angle));
                frontGunIndex--;
            }

            if (frontGunIndex > pam.sideCount - 1)
                frontGunIndex = 0;
            if (frontGunIndex < 0)
                frontGunIndex = pam.sideCount - 1;
            canRotateGuns = false;
            if (pam.guns[frontGunIndex] != null)
            {
                pam.guns[frontGunIndex].GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
            }
            StartCoroutine("ScrollDelay");
            UpdateUIElements();
        }
        

        
    }

    public void SetGunPositions()
    {
        //frontGunIndex = 0;
        //print("Gun Capacity " + pam.guns.Count);
        print("Front gun should be: " + pam.guns[frontGunIndex]);
        float angle = 360f / pam.sideCount;
        for (int i = 0; i < pam.guns.Count; i++)
        {
            //fornt gun index starts it so that the first gun will always be the one selected 
            //rather than resetting to the first in the list
            //print("Frotn Gun Index = " + frontGunIndex);
            if(i+frontGunIndex < pam.guns.Count)
            {
                if (i + frontGunIndex < 0)
                    print("FrontGunIndex is zero!!!!!!");

                if (pam.guns[i + frontGunIndex] != null)
                {
                    pam.guns[i + frontGunIndex].transform.position = front.transform.position;
                    pam.guns[i + frontGunIndex].transform.rotation = front.transform.rotation;
                }
            }
            else
            {
                if (pam.guns[(i + frontGunIndex) - pam.guns.Count] != null)
                {
                    pam.guns[(i + frontGunIndex) - pam.guns.Count].transform.position = front.transform.position;
                    pam.guns[(i + frontGunIndex) - pam.guns.Count].transform.rotation = front.transform.rotation;
                }
            }
            transform.Rotate(new Vector3(0, 0, -angle));
        }
        UpdateUIElements();
        //print("Updated Guns");
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
}
