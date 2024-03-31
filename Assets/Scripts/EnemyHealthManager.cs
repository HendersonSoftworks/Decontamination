using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{
    public float health;
    public Slider slider;
    public EnemyMovementController enemyMovement;
    public PlayerCombatController combatController;
    public GameObject bloodSplatter;

    public enum Weaknesses { water, fire, acid}
    public Weaknesses weakness;

    private void Start()
    {
        combatController = FindAnyObjectByType<PlayerCombatController>();
    }

    void Update()
    {
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
            float currentDamage = combatController.weaponDamages[(int)combatController.currentWeapon];
            
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
