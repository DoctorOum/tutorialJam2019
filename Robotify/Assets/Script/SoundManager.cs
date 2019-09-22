using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] shootingClips;
    public AudioSource source;

    private void Update()
    {
        source.PlayOneShot(shootingClips[1]);
    }
}
