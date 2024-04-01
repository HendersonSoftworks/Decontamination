using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    public GameObject fuelText;
    public enum FuelTypes { water, fire, acid}
    public FuelTypes fuelType;

    PlayerCombatController combatController;
    SpriteRenderer spriteRenderer;
    PlayerUIManager playerUIManager;

    private AudioSource audioSource;
    private BoxCollider2D boxCollider2D; 

    private void Start()
    {
        combatController = FindObjectOfType<PlayerCombatController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerUIManager = FindObjectOfType<PlayerUIManager>();
        audioSource = GetComponent<AudioSource>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        SetFuel(fuelType);
    }

    public void SetFuel(FuelTypes _fuelType)
    {
        fuelType = _fuelType;

        switch (fuelType)
        {
            case FuelTypes.water:
                spriteRenderer.color = playerUIManager.uiColours[1];
                break;
            case FuelTypes.fire:
                spriteRenderer.color = playerUIManager.uiColours[3];
                break;
            case FuelTypes.acid:
                spriteRenderer.color = playerUIManager.uiColours[5];
                break;
            default:
                break;
        }
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
            combatController.AddFuel(100, (int)fuelType);
            audioSource.PlayOneShot(audioSource.clip);
            spriteRenderer.enabled = false;
            boxCollider2D.enabled = false;
            StartCoroutine(DelayDestroy());
        }
    }
}
