using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public GameObject fuelText;

    PlayerCombatController combatController;
    SpriteRenderer spriteRenderer;

    private AudioSource audioSource;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        combatController = FindObjectOfType<PlayerCombatController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Instantiate(fuelText, transform.position, Quaternion.identity);
            if (combatController.healthPacks < 8)
            {
                combatController.healthPacks++;
            }
            audioSource.PlayOneShot(audioSource.clip);
            spriteRenderer.enabled = false;
            boxCollider2D.enabled = false;
            StartCoroutine(DelayDestroy());
        }
    }

}
