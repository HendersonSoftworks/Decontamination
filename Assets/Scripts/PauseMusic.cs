using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMusic : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicAudioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        musicAudioSource.Stop();
    }
}
