using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPaused;
    public GameObject pauseMenu;

    [SerializeField]
    private GameObject player;

    private PlayerCombatController playerCombat;

    void Start()
    {
        player = FindAnyObjectByType<PlayerCombatController>().gameObject;
        playerCombat = player.GetComponent<PlayerCombatController>();

        ClosePauseMenu();
    }

    void Update()
    {
        CheckHealth();
        ManagePauseMenu();
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
