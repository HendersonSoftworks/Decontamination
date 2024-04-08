using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static NPC currentNPC;

    public bool isPaused;
    public GameObject pauseMenu;
    public GameObject levelCompleteMenu;
    public List <Pickup> pickups;
    public GameObject player;

    [SerializeField]
    private TextMeshProUGUI remainingTextMesh;

    private int initialPickupNum;
    private PlayerCombatController playerCombat;

    void Start()
    {
        player = FindAnyObjectByType<PlayerCombatController>().gameObject;
        playerCombat = player.GetComponent<PlayerCombatController>();
        RefreshPickupsArray();
        initialPickupNum = pickups.Count;
        SetRemainingPickupUI();
        ClosePauseMenu();
    }

    void Update()
    {
        CheckHealth();
        ManagePauseMenu();
    }

    public void OpenLevelCompleteScreen()
    {
        isPaused = true;
        levelCompleteMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadHub()
    {
        SceneManager.LoadScene("Hub");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RefreshPickupsArray()
    {
        pickups.Clear();

        Pickup[] tempPickups = FindObjectsOfType<Pickup>();
        foreach (Pickup pickup in tempPickups)
        {
            if (pickup.isIrradiated)
            {
                pickups.Add(pickup);
            }
        }
    }

    public void SetRemainingPickupUI()
    {
        remainingTextMesh.text = pickups.Count + " I " + initialPickupNum;
    }

    private void ManagePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ClosePauseMenu();
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }

    private void OpenPauseMenu()
    {
        if (levelCompleteMenu.activeInHierarchy)
        {
            return;
        }

        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void ClosePauseMenu()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void CheckHealth()
    {
        if (playerCombat.healthPacks <= 0)
        {
            print("Player dead");
        }
    }
}
