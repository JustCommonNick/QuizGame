using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public AudioSource _AudioSource;
    public bool IsPrefabButton;
    public AudioSource _AudioSourceForPrefabButton => GameObject.Find("ButtonMusicEvent").GetComponent<AudioSource>();
    public void ClickSound()
    {
        if (IsPrefabButton)
        {
            _AudioSourceForPrefabButton.Play();
        }
        {
            _AudioSource.Play();
        }
        
    }

}
