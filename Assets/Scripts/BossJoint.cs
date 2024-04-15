using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using System;

public class BossJoint : MonoBehaviour
{
    private PlayerCombatController combatController;
    private EnemyHealthManager enemyHealth;
    private BrainBossController bossController;

    [SerializeField]
    private bool isDestroyed = false;

    public float health;
    //public Slider slider;
    //public Canvas canvas;
    public enum Weaknesses { water, fire, acid, none }
    public Weaknesses currentWeakness;

    void Start()
    {
        combatController = FindAnyObjectByType<PlayerCombatController>();
        enemyHealth = GetComponentInParent<EnemyHealthManager>();
        bossController = GetComponentInParent<BrainBossController>();
    }

    private void Update()
    {
        //slider.value = health;
        //canvas.gameObject.SetActive(false);

        if (health <= 0)
        {
            DestroyJoint();
        }
    }

    private void DestroyJoint()
    {
        if (isDestroyed)
        {
            return;
        }

        isDestroyed = true;

        GameObject bloodSplatter = Instantiate(enemyHealth.bloodSplatter, transform.position, Quaternion.identity);
        bloodSplatter.transform.localScale = bloodSplatter.transform.localScale * 3;
        bossController.jointCount--;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDestroyed)
        {
            return;
        }

        if (collision.tag == "Weapon")
        {
            //canvas.gameObject.SetActive(true);

            float currentDamage = combatController.weaponDamages[(int)combatController.currentWeapon] * Time.deltaTime;

            if ((int)combatController.currentWeapon == (int)currentWeakness)
            {
                health -= currentDamage * combatController.weaponDamageMultipliers[1];
                bossController.health -= currentDamage * combatController.weaponDamageMultipliers[1];
            }
            else
            {
                health -= currentDamage;
                bossController.health -= currentDamage;
            }
        }
    }
}
