using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public AudioClip[] audio;
    private AudioSource audioSrc => GetComponent<AudioSource>();
    public void PlaySound(AudioClip clip, float volume = 1f,bool destroyed = false, float p1 = 1f, float p2 = 1f)
    {
        audioSrc.PlayOneShot(clip, volume);
    }

}
