using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Pickup : MonoBehaviour
{
    public float audioMaxDist;
    public float distFromPlayer;
    public bool isIrradiated;

    private PlayerCombatController combatController;
    private Light2D light2D;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private GameManager gameManager;

    void Start()
    {
        combatController = FindAnyObjectByType<PlayerCombatController>();
        light2D = GetComponent<Light2D>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        gameManager = FindAnyObjectByType<GameManager>();

        isIrradiated = true;
    }

    void Update()
    {
        light2D.intensity = Mathf.Clamp(light2D.intensity, 0, 1000);

        if (light2D.intensity == 0)
        {
            audioSource.enabled = false;
        }

        distFromPlayer = Vector2.Distance(transform.position, combatController.transform.position);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Weapon" && combatController.currentWeapon == 0)
        {
            float currentDamage = combatController.weaponDamages[(int)combatController.currentWeapon] * Time.deltaTime;

            light2D.intensity -= (currentDamage * combatController.weaponDamageMultipliers[0]) / 10;

            Vector2 moveDir = -(combatController.transform.position - transform.position).normalized;
            rb.velocity = (moveDir * 1);

            if (light2D.intensity <= 0 && isIrradiated)
            {
                isIrradiated = false;
                gameManager.RefreshPickupsArray();
                gameManager.SetRemainingPickupUI();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        rb.velocity = Vector2.zero;
    }
}
