using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject dialoguePopup;
    [SerializeField]
    private GameObject missionScreen;

    private PlayerMovementController playerMovement;

    void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovementController>();

        dialoguePopup.SetActive(false);
    }

    void Update()
    {
        if (!playerMovement.inDialogue && dialoguePopup.activeInHierarchy)
            if (Input.GetKeyDown(KeyCode.Space))
                OpenMissionScreen();
    }

    private void OpenMissionScreen()
    {
        playerMovement.inDialogue = true;
        playerMovement.SetVelocityToZero();
        missionScreen.SetActive(true);
    }

    public void CloseMissionScreen()
    {
        playerMovement.inDialogue = false;
        missionScreen.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialoguePopup.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialoguePopup.SetActive(false);
        }
    }
}
