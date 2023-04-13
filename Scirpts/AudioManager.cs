using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;

    
    public void OnClickCard()
    {
        audioSource.PlayOneShot(audioClip);
    }
    

    
}
