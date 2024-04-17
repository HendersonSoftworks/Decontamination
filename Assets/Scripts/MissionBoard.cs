using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MissionBoard : MonoBehaviour
{
    public static bool interactingWithMissionBoard;

    [SerializeField]
    private GameObject dialoguePopup;
    [SerializeField]
    private GameObject missionScreen;
    [SerializeField]
    private GameObject[] previewImages;
    [SerializeField]
    private string[] missionDescs;
    [SerializeField]
    private string currentSelectedMission;
    [SerializeField]
    private TextMeshProUGUI missionText;
    [SerializeField]
    private Button[] levelButtons; 

    private PlayerMovementController playerMovement;

    void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovementController>();
        dialoguePopup.SetActive(false);
        SetLevel1();

        // Check Player progress to determine options
        if (!ProgressManager.Level1Complete)
        { 
            levelButtons[1].interactable = false; 
        }
        else
        {
            levelButtons[1].interactable = true;
        }

        if (!ProgressManager.Level2Complete)
        {
            levelButtons[2].interactable = false;
        }
        else
        {
            levelButtons[2].interactable = true;
        }
    }

    void Update()
    {
        if (!playerMovement.inDialogue && dialoguePopup.activeInHierarchy)
            if (Input.GetKeyDown(KeyCode.Space))
                OpenMissionScreen();
    }

    public void LoadMission()
    {
        CloseMissionScreen();
        SceneManager.LoadScene(currentSelectedMission);
    }

    public void SetLevel1()
    {
        previewImages[0].SetActive(true);
        previewImages[1].SetActive(false);
        previewImages[2].SetActive(false);
        missionText.text = missionDescs[0];

        currentSelectedMission = "Level1";
    }

    public void SetLevel2()
    {
        previewImages[0].SetActive(false);
        previewImages[1].SetActive(true);
        previewImages[2].SetActive(false);
        missionText.text = missionDescs[1];

        currentSelectedMission = "Level2";
    }

    public void SetLevel3()
    {
        previewImages[0].SetActive(false);
        previewImages[1].SetActive(false);
        previewImages[2].SetActive(true);
        missionText.text = missionDescs[2];

        currentSelectedMission = "Level3";
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
            interactingWithMissionBoard = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialoguePopup.SetActive(false);
            interactingWithMissionBoard = false;
        }
    }
}
