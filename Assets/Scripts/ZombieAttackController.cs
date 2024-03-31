using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackController : MonoBehaviour
{
    [SerializeField]
    public float pushForce;

    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private EnemyMovementController enemyMovement;
    private EnemyAnimationManager enemyAnimation;
    private GameObject player;
    private PlayerCombatController PlayerCombatController;
    private PlayerMovementController PlayerMovementController;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyMovement = GetComponent<EnemyMovementController>();
        enemyAnimation = GetComponent<EnemyAnimationManager>();
        player = FindObjectOfType<PlayerCombatController>().gameObject;
        PlayerCombatController = player.GetComponent<PlayerCombatController>();
        PlayerMovementController = player.GetComponent<PlayerMovementController>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyAnimation.SetAttackBool(true);
        }
    }

    private IEnumerator TemporarilyDisableCollider()
    {
        boxCollider2D.enabled = false;
        yield return new WaitForSeconds(0.1f);
        boxCollider2D.enabled = true;
    }

    public void AttackPlayer()
    {
        // Damage player if not invicible
        if (!PlayerCombatController.isInvincible)
        {
            PlayerCombatController.TakeDamage();
            StartCoroutine(TemporarilyDisableCollider());
        }

        // Push player back
        if (spriteRenderer.flipX)
        {
            //player.transform.position = new Vector2(player.transform.position.x - 0.5f,
            //    player.transform.position.y);
            PlayerMovementController.PushPlayer(Vector2.left, pushForce);
        }
        if (!spriteRenderer.flipX)
        {
            //player.transform.position = new Vector2(player.transform.position.x + 0.5f,
            //    player.transform.position.y);
            PlayerMovementController.PushPlayer(Vector2.right, pushForce);
        }

        enemyAnimation.SetAttackBool(false);

    }
}
