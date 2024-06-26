using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    #region Public
    public float speed;
    public GameObject player;
    public bool inContactWithPlayer = false;
    public bool beingHit = false;
    public bool canMove;
    public enum MoveStates { idle, chasing }
    public MoveStates currentMoveState;
    public float playerDist;
    public float moveToplayerDist;
    #endregion

    #region Private
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerCombatController>().gameObject;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            ManageMoveState();
            ManageMovement();
        }
    }

    private void ManageMoveState()
    {
        playerDist = Vector2.Distance(player.transform.position, transform.position);
        if (playerDist < moveToplayerDist)
        {
            currentMoveState = MoveStates.chasing;
        }
        else
        {
            currentMoveState = MoveStates.idle;
        }
    }

    public void ManageMovement()
    {
        if (currentMoveState == MoveStates.idle)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (beingHit)
        {
            Vector2 moveDir = -(player.transform.position - transform.position).normalized;
            rb.velocity = (moveDir * 1);
            return;
        }

        // Move in player direction
        if (inContactWithPlayer)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            Vector2 moveDir = (player.transform.position - transform.position).normalized;
            rb.velocity = (moveDir * speed);
            // Change animation depending on horizontal value
            ManageAnimation(moveDir.x);
        }
    }

    private void ManageAnimation(float hAxis)
    {
        if (hAxis < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (hAxis > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void FreezeConstraints()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void ReleaseConstraints()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void SetVelocityToZero()
    {
        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inContactWithPlayer = true;
            FreezeConstraints();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inContactWithPlayer = false;
            ReleaseConstraints();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            beingHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            beingHit = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, moveToplayerDist);
    }
}
