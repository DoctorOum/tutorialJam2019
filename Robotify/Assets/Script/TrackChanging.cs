using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackChanging : MonoBehaviour
{
    GameObject player;
    int playerSize;

    public AudioClip a;
    public AudioClip b;
    public AudioClip c;

    public AudioClip death;
    public Canvas deathScene;
    bool isDead = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerSize = player.GetComponent<playerAttackManager>().sideCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<playerAttackManager>().sideCount == 3 && playerSize != player.GetComponent<playerAttackManager>().sideCount)
        {
            playerSize = 3;
            gameObject.GetComponent<AudioSource>().clip = a;
            gameObject.GetComponent<AudioSource>().Play();
        }
        else if(player.GetComponent<playerAttackManager>().sideCount == 5 && playerSize != player.GetComponent<playerAttackManager>().sideCount)
        {
            playerSize = 5;
            gameObject.GetComponent<AudioSource>().clip = b;
            gameObject.GetComponent<AudioSource>().Play();
        }
        else if(player.GetComponent<playerAttackManager>().sideCount == 10 && playerSize != player.GetComponent<playerAttackManager>().sideCount)
        {
            playerSize = 10;
            gameObject.GetComponent<AudioSource>().clip = c;
            gameObject.GetComponent<AudioSource>().Play();
        }
        if(deathScene.gameObject.activeInHierarchy && !isDead)
        {
            gameObject.GetComponent<AudioSource>().clip = death;
            gameObject.GetComponent<AudioSource>().Play();
            isDead = true;
        }
    }
}
