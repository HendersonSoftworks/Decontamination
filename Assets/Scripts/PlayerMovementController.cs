using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("WORLD")]
    public bool canMove = true;
    public Vector2 position;

    public enum weapons 
    { 
        PowerWasher,
        Flamethrower,
    };

    [Header("COMBAT")]
    [SerializeField]
    private GameObject weaponObj;
    
    [SerializeField]
    private weapons currentWeapon;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float[] weaponDamages;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        position = transform.position;

        ManageAnimation();

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            ManageMovement();
        }
    }

    private void ManageAnimation()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            FlipWeaponX(true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            FlipWeaponX(false);
        }
    }

    private void FlipWeaponX(bool isFlipped)
    {
        SpriteRenderer weaponSprite = weaponObj.GetComponent<SpriteRenderer>();

        if (isFlipped)
        {
            weaponObj.transform.localPosition = new Vector2(-2, weaponObj.transform.localPosition.y);
            weaponSprite.flipX = true;
        }
        else
        {
            weaponObj.transform.localPosition = new Vector2(2, weaponObj.transform.localPosition.y);
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            print("OOF!");
            rb.velocity = Vector2.zero;
        }
    }
}
