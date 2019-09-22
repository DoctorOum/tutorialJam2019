using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackManager : MonoBehaviour
{
    public int ammo;
    public int sideCount = 4;
    public GunPositionManager gpm;
    public List<GameObject> guns;
    private AudioSource outOfAmmo;
    private bool soundDelay;
    private ProgressionManager pm;

    private void Start()
    {
        pm = GameObject.FindGameObjectWithTag("ProgressionManager").GetComponent<ProgressionManager>();
        outOfAmmo = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if(ammo > 0)
            {
                FireGuns();
            }
            else if(!soundDelay)
            {
                StartCoroutine("OutOfAmmoDelay");
            }
            
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
    IEnumerator OutOfAmmoDelay()
    {
        soundDelay = true;
        outOfAmmo.Play();
        yield return new WaitForSeconds(0.2f);
        soundDelay = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            pm.Hit(collision.gameObject.GetComponent<Projectile>().damage);
        }
    }
}
