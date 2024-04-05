using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    // Weapons
    public enum Weapons
    {
        PowerWasher,
        FlameThrower,
        AcidBlaster
    };

    public GameObject[] weaponObjects;
    public GameObject currentWeaponObject;
    public Weapons currentWeapon;
    public float[] weaponDamages;
    public float[] weaponDamageMultipliers;
    public float[] weaponFuels;

    // Health
    public bool isInvincible;
    public float invincibilityWindow;
    public int healthPacks;

    // References
    private PlayerMovementController movementController;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        movementController = GetComponent<PlayerMovementController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StopAttack();

        // Set water fuel to 100 and the rest to 0
        AddFuel(100, (int)Weapons.PowerWasher);
    }

    void Update()
    {
        ClampFuels();
        ManageWeaponSelection();
        ManageWeaponObject();

        if (Input.GetKey(KeyCode.Space))
        {
            if (weaponFuels[(int)currentWeapon] > 0)
            {
                StartAttack();
            }
            else
            {
                StopAttack();
            }
        }
        else
        {
            StopAttack();
            RegenerateWater();
        }
    }

    private void RegenerateWater()
    {
        weaponFuels[(int)Weapons.PowerWasher] += 0.05f;
    }

    public void TakeDamage()
    {
        healthPacks--;
        isInvincible = true;
        StartCoroutine(Invincibility());
    }

    private IEnumerator Invincibility()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(invincibilityWindow);
        isInvincible = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void AddFuel(float amount, int weaponType)
    {
        weaponFuels[weaponType] += amount;
    }

    private void StartAttack()
    {
        CorrectWeaponHAxis();
        currentWeaponObject.SetActive(true);
        movementController.SetVelocityToZero();
        movementController.canMove = false;
        ReduceWeaponFuel();
    }

    private void StopAttack()
    {
        currentWeaponObject.SetActive(false);
        movementController.canMove = true;
    }

    private void ReduceWeaponFuel()
    {
        AddFuel(-0.1f, (int)currentWeapon);
    }

    private void ManageWeaponSelection()
    {
        if (!movementController.canMove)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentWeapon = Weapons.PowerWasher;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            currentWeapon = Weapons.FlameThrower;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            currentWeapon = Weapons.AcidBlaster;
    }

    private void ManageWeaponObject()
    {
        switch (currentWeapon)
        {
            case Weapons.PowerWasher:
                currentWeaponObject = weaponObjects[0];
                break;
            case Weapons.FlameThrower:
                currentWeaponObject = weaponObjects[1];
                break;
            case Weapons.AcidBlaster:
                currentWeaponObject = weaponObjects[2];
                break;
            default:
                break;
        }
    }

    private void CorrectWeaponHAxis()
    {
        if (spriteRenderer.flipX)
        {
            movementController.FlipWeaponX(true);
        }
        else
        {
            movementController.FlipWeaponX(false);
        }
    }

    private void ClampFuels()
    {
        for (int i = 0; i < weaponFuels.Length; i++)
        {
            weaponFuels[i] = Mathf.Clamp(weaponFuels[i], 0, 100);
        }
    }
}
