using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    private EnemyMovementController enemyMovement;
    private Animator animator;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovementController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (enemyMovement.currentMoveState == EnemyMovementController.MoveStates.idle)
        {
            animator.SetBool("isWalking", false);
        }
        if (enemyMovement.currentMoveState == EnemyMovementController.MoveStates.chasing)
        {
            animator.SetBool("isWalking", true);
        }
    }

    public void SetAttackBool(bool isAttacking)
    {
        animator.SetBool("isAttacking", isAttacking);
    }
}
