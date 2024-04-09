using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public int lineNum;
    public string[] lines;

    [SerializeField]
    private GameObject dialogueIcon;
    [SerializeField]
    private NPCDialogue npcDialogue;
    [SerializeField]
    private TextMeshProUGUI dialogueTMP;
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private string currentLine;
    [SerializeField]
    private Sprite portrait;
    [SerializeField]
    private Image portraitUI;

    private PlayerMovementController playerMovement;

    void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovementController>();
        GameManager.currentNPC = null;
    }

    private void Update()
    {
        if (this != GameManager.currentNPC)
            return;

        if (!playerMovement.inDialogue && dialogueIcon.activeInHierarchy)
            if (Input.GetKeyDown(KeyCode.Space))
                EnterDialogueState();
        
        if (playerMovement.inDialogue)
            if (Input.GetKeyDown(KeyCode.Space))
                PlayNextLine();
    }

    private void EnterDialogueState()
    {
        playerMovement.inDialogue = true;
        playerMovement.SetVelocityToZero();
        portraitUI.sprite = portrait;
    }

    private void PlayNextLine()
    {
        if (lineNum < lines.Length)
        {
            dialoguePanel.SetActive(true);
            npcDialogue.AnimateText();
            lineNum++;
        }
        else
        {
            ResetLines();
            ClosePanel();
        }
    }

    public void ResetLines()
    {
        lineNum = 0;
    }

    public void ClosePanel()
    {
        dialoguePanel.SetActive(false);
        npcDialogue.ClearLines();
        GameManager.currentNPC = null;
        StartCoroutine(EnableAttack());
    }

    private IEnumerator EnableAttack()
    {
        yield return new WaitForSeconds(0.25f);
        playerMovement.inDialogue = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialogueIcon.SetActive(true);
            GameManager.currentNPC = this;
            npcDialogue.currentNPC = GameManager.currentNPC.GetComponent<NPC>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialogueIcon.SetActive(false);
        }
    }
}
