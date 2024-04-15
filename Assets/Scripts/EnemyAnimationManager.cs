using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    private EnemyMovementController enemyMovement;
    private Animator animator;
    public List<GameObject> collidingObjects = new List<GameObject>();

    [SerializeField]
    private GameObject healthCanvas;

    private BossJoint[] bossJoints;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovementController>();
        animator = GetComponent<Animator>();
        healthCanvas.SetActive(false);

        bossJoints = GetComponentsInChildren<BossJoint>();
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

        //if (bossJoints != null)
        //{
        //    RevealJointCanvas();
        //}
        
        RevealHealthCanvas();
    }

    //private void RevealJointCanvas()
    //{
    //    for (int i = collidingObjects.Count - 1; i >= 0; i--)
    //    {
    //        if (collidingObjects[i].tag == "Weapon")
    //        {
    //            foreach (var joint in bossJoints)
    //            {
    //                joint.canvas.gameObject.SetActive(true);
    //            }
    //        }

    //        if (collidingObjects[i] == null)
    //        {
    //            collidingObjects.RemoveAt(i);
    //        }
    //    }

    //    if (collidingObjects.Count <= 0)
    //    {
    //        foreach (var joint in bossJoints)
    //        {
    //            joint.canvas.gameObject.SetActive(false);
    //        }
    //    }
    //}

    private void RevealHealthCanvas()
    {
        for (int i = collidingObjects.Count - 1; i >= 0; i--)
        {
            if (collidingObjects[i].tag == "Weapon")
            {
                healthCanvas.SetActive(true);
            }

            if (collidingObjects[i] == null)
            {
                collidingObjects.RemoveAt(i);
            }
        }

        if (collidingObjects.Count <= 0)
        {
            healthCanvas.SetActive(false);
        }
    }

    public void SetAttackBool(bool isAttacking)
    {
        animator.SetBool("isAttacking", isAttacking);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidingObjects.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collidingObjects.Remove(collision.gameObject);
    }
}