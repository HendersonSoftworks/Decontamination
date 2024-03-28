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
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            float currentDamage = combatController.weaponDamages[(int)combatController.currentWeapon];
            health -= currentDamage;

            //switch (combatController.currentWeapon)
            //{
            //    case PlayerCombatController.Weapons.PowerWasher:
            //        break;
            //    case PlayerCombatController.Weapons.FlameThrower:
            //        break;
            //    case PlayerCombatController.Weapons.AcidBlaster:
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
