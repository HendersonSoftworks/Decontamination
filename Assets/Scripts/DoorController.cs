using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private Transform telePoint;

    private Animator animator;
    private Collider2D col;
    private AudioSource audioSource;

    [SerializeField]
    private PlayerMovementController playerMoveController;
    [SerializeField]
    private EnemyMovementController[] enemyMoveControllers;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        playerMoveController = FindAnyObjectByType<PlayerMovementController>();
        enemyMoveControllers = FindObjectsOfType<EnemyMovementController>();
    }

    public void TeleportPlayer()
    {
        col.gameObject.transform.position = telePoint.position;
        PlayActors();
    }

    public void PauseActors()
    {
        playerMoveController.isPaused = true;
        playerMoveController.SetVelocityToZero();
        playerMoveController.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        foreach (var enemy in enemyMoveControllers)
        {
            enemy.canMove = false;
            enemy.SetVelocityToZero();
        }
    }

    public void PlayActors()
    {
        playerMoveController.isPaused = false;
        playerMoveController.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        foreach (var enemy in enemyMoveControllers)
        {
            enemy.canMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            col = collision;
            animator.SetTrigger("Open");
            audioSource.PlayOneShot(audioSource.clip);

            PauseActors();
        }
    }
}
