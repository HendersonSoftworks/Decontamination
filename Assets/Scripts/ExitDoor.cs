using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    private PlayerMovementController playerMoveController;
    private GameManager gameManager;

    [SerializeField]
    private WarningText warningText;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerMoveController = FindAnyObjectByType<PlayerMovementController>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public IEnumerator DelayOpenCompleteMenu()
    {
        yield return new WaitForSeconds(10f);
        gameManager.OpenLevelCompleteScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && gameManager.pickups.Count > 0)
        {
            warningText.AnimateWarningText();
        }

        if (collision.tag == "Player" && gameManager.pickups.Count <= 0)
        {
            warningText.AnimateSuccessText();
            StartCoroutine(DelayOpenCompleteMenu());
        }
    }
}
