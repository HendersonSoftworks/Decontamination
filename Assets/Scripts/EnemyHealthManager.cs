using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{
    public float health;
    public Slider slider;
    public EnemyMovementController enemyMovement;
    public PlayerCombatController combatController;
    public GameObject bloodSplatter;

    public enum Weaknesses { water, fire, acid, none}
    public Weaknesses weakness;
    public Light2D light2D;

    private void Start()
    {
        combatController = FindAnyObjectByType<PlayerCombatController>();
        light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        if (light2D != null)
        {
            light2D.intensity = Mathf.Clamp(light2D.intensity, 0, 1000);
            
            if (light2D.intensity > 0)
            {
                weakness = Weaknesses.none;
            }
            else
            {
                weakness = Weaknesses.fire;
            }
        }
        
        slider.value = health;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(bloodSplatter, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            float currentDamage = combatController.weaponDamages[(int)combatController.currentWeapon] * Time.deltaTime;

            // Irradiated enemy - doesn't take damage until the radiation has been cleansed
            if (light2D != null && light2D.intensity > 0)
            {
                if (combatController.currentWeapon == 0)
                {
                    light2D.intensity -= (currentDamage * combatController.weaponDamageMultipliers[0]) / 10;
                }
                
                return;
            }
            
            if ((int)combatController.currentWeapon == (int)weakness)
            {
                health -= currentDamage * combatController.weaponDamageMultipliers[1];
            }
            else
            {
                health -= currentDamage;
            }
        }
    }
}
