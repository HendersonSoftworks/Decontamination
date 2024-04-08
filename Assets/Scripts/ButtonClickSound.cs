using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public AudioClip buttonClickClip;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip floppyClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickClip);
    }

    public void PlayFloppySound()
    {
        audioSource.PlayOneShot(floppyClip);
    }
}
