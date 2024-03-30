using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
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
    public float[] weaponFuels;
    public int healthPacks;

    private PlayerMovementController movementController;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        movementController = GetComponent<PlayerMovementController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StopAttack();

        // Set water fuel to 100 and the rest to 0
        SetFuel(100, 0, 0);
    }

    void Update()
    {
        // Clamp fuels
        foreach (float fuel in weaponFuels)
        {
            Mathf.Clamp(fuel, 0, 100);
        }

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
                print("No fuel!");
            }
        }
        else
        {
            StopAttack();
        }
    }

    public void SetFuel(float water, float fire, float acid)
    {
        weaponFuels[0] += water;
        weaponFuels[1] += fire;
        weaponFuels[2] += acid;
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
        switch (currentWeapon)
        {
            case Weapons.PowerWasher:
                SetFuel(-0.1f, 0, 0);
                break;
            case Weapons.FlameThrower:
                SetFuel(0, -0.1f, 0);
                break;
            case Weapons.AcidBlaster:
                SetFuel(0, 0, -0.1f);
                break;
            default:
                break;
        }
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
}
