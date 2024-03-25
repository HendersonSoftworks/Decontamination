using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("WORLD STATS")]
    public bool canMove = true;
    public Vector2 position;

    public enum weapons 
    { 
        PowerWasher,
        Flamethrower,
    };

    [Header("COMBAT STATS")]
    [SerializeField]
    private weapons currentWeapon;


    [SerializeField]
    private float speed;

    [SerializeField]
    private float[] weaponDamages;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
