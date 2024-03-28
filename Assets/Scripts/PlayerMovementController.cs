using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public bool canMove = true;
    public Vector2 position;

    [SerializeField]
    private float speed;

    // Private properties
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PlayerCombatController combatController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        combatController = GetComponent<PlayerCombatController>();
    }

    void Update()
    {
        position = transform.position;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            ManageMovement();
        }
    }

    private void ManageAnimation(float hAxis)
    {
        if (hAxis < 0)
        {
            spriteRenderer.flipX = true;
            FlipWeaponX(true);
        }
        if (hAxis > 0)
        {
            spriteRenderer.flipX = false;
            FlipWeaponX(false);
        }
    }

    public void FlipWeaponX(bool isFlipped)
    {
        SpriteRenderer weaponSprite = combatController.currentWeaponObject.GetComponent<SpriteRenderer>();

        if (isFlipped)
        {
            combatController.currentWeaponObject.transform.localPosition = new Vector2(-2, combatController.currentWeaponObject.transform.localPosition.y);
            weaponSprite.flipX = true;
        }
        else
        {
            combatController.currentWeaponObject.transform.localPosition = new Vector2(2, combatController.currentWeaponObject.transform.localPosition.y);
            weaponSprite.flipX = false;
        }
    }

    public void ManageMovement()
    {
        // Get move direction
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");
        Vector2 moveDir = new Vector2(hAxis, vAxis);

        // Move player in direction
        //transform.Translate(moveDir * Time.deltaTime * speed);
        rb.velocity = (moveDir * speed);

        // Change animation depending on horizontal value
        ManageAnimation(hAxis);
    }

    public void SetVelocityToZero()
    {
        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            SetVelocityToZero();
        }
    }
}
