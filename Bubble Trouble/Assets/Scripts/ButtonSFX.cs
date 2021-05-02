using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
   // public AudioClip bubbleSFX; //the Bubble SFX

    //Reference to the AudioSource attached to the button
    private AudioSource buttonSource;

    private void Start()
    {
        buttonSource = GetComponent<AudioSource>();
        buttonSource.volume = Settings.volume;
    }
    //Play the Bubble SFX when tapping on a Button
    public void TapButtonSFX()
    {
        buttonSource.Play(); 
    }
}
