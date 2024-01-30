using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public AudioClip clickFx;

    public void ClickSound()
    {
        GetComponent<AudioSource>().PlayOneShot(clickFx);
    }

}
