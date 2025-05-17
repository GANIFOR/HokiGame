using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip buttonSnd;
    public AudioClip clickSnd;


    public void HoverAudio()
    {
        myFX.PlayOneShot(buttonSnd);

    }
    public void ClickAudio()
    {

        
        myFX.PlayOneShot(clickSnd);
    }
}
