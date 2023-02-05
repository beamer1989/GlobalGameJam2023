using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEffects : MonoBehaviour
{
    public AudioClip deliver;
    public AudioClip shoot;
    public AudioClip sun;
    public AudioClip water;

    public void playDeliver()
    {
        GetComponent<AudioSource>().PlayOneShot(deliver);
    }
    
    public void playShoot()
    {
        GetComponent<AudioSource>().PlayOneShot(shoot);
    }
    
    public void playSun()
    {
        GetComponent<AudioSource>().PlayOneShot(sun);
    }
    
    public void playWater()
    {
        GetComponent<AudioSource>().PlayOneShot(water);
    }

}
