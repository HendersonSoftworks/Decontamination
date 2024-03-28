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

    [SerializeField]
    private Weapons currentWeapon;

    [SerializeField]
    private float[] weaponDamages;

    private PlayerMovementController movementController;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        movementController = GetComponent<PlayerMovementController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StopAttack();
    }

    void Update()
    {
        ManageWeaponSelection();
        ManageWeaponObject();

        if (Input.GetKey(KeyCode.Space))
        {
            StartAttack();
        }
        else
        {
            StopAttack();
        }
    }

    private void StartAttack()
    {
        CorrectWeaponHAxis();
        currentWeaponObject.SetActive(true);
        movementController.SetVelocityToZero();
        movementController.canMove = false;
    }

    private void StopAttack()
    {
        currentWeaponObject.SetActive(false);
        movementController.canMove = true;
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
