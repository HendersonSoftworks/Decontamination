using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private EnemyMovementController enemyMovement;
    private EnemyAnimationManager enemyAnimation;
    private GameObject player;
    private PlayerCombatController PlayerCombatController;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyMovement = GetComponent<EnemyMovementController>();
        enemyAnimation = GetComponent<EnemyAnimationManager>();
        player = FindObjectOfType<PlayerCombatController>().gameObject;
        PlayerCombatController = player.GetComponent<PlayerCombatController>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyAnimation.SetAttackBool(true);
        }
    }

    public void AttackPlayer()
    {
        // Reduce player health
        PlayerCombatController.healthPacks--;
        // Push player back
        if (spriteRenderer.flipX)
        {
            player.transform.position = new Vector2(player.transform.position.x - 0.5f,
                player.transform.position.y);
        }
        if (!spriteRenderer.flipX)
        {
            player.transform.position = new Vector2(player.transform.position.x + 0.5f,
                player.transform.position.y);
        }

        enemyAnimation.SetAttackBool(false);
    }
}
