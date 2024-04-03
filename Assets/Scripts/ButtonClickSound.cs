using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip buttonClickClip;
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
