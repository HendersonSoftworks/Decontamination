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
    public List <Pickup> contaminants;
    public GameObject player;
    public static bool isPlayerDead = false;
    public GameObject[] drops;

    [SerializeField]
    private TextMeshProUGUI remainingTextMesh;
    [SerializeField]
    private WarningText warningText;
    [SerializeField]
    private GameObject bloodSplatter;

    private int initialPickupNum;
    private PlayerCombatController playerCombat;

    void Start()
    {
        isPlayerDead = false;
        player = FindAnyObjectByType<PlayerCombatController>().gameObject;
        playerCombat = player.GetComponent<PlayerCombatController>();
        warningText = FindAnyObjectByType<WarningText>();

        RefreshPickupsArray();
        initialPickupNum = contaminants.Count;
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
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                ProgressManager.MarkLevel1Complete();
                break;
            case "Level2":
                ProgressManager.MarkLevel2Complete();
                break;
            case "Level3":
                ProgressManager.MarkLevel3Complete();
                break;
            default:
                break;
        }
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
        contaminants.Clear();

        Pickup[] tempPickups = FindObjectsOfType<Pickup>();
        foreach (Pickup pickup in tempPickups)
        {
            if (pickup.isIrradiated)
            {
                contaminants.Add(pickup);
            }
        }
    }

    public void SetRemainingPickupUI()
    {
        remainingTextMesh.text = contaminants.Count + " I " + initialPickupNum;
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

    public void ClosePauseMenu()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void CheckHealth()
    {
        if (playerCombat.healthPacks <= 0 && !isPlayerDead)
        {
            isPlayerDead = true;
            player.GetComponent<PlayerMovementController>().inDialogue = true;
            player.GetComponent<SpriteRenderer>().enabled = false;
            warningText.AnimateDeathText();
            Instantiate(bloodSplatter, player.transform.position, Quaternion.identity);
            StartCoroutine(RetryLevel());
        }
    }

    private IEnumerator RetryLevel()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
