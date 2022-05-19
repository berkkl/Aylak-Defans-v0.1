using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClickSound : MonoBehaviour
{
    public AudioSource clickSound;
    

    public void playClickSound()
    {
        clickSound.Play();
    }
}
