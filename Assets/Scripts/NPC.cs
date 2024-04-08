using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueIcon;
    [SerializeField]
    private NPCDialogue npcDialogue;
    [SerializeField]
    private TextMeshProUGUI dialogueTMP;
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private int lineNum;
    [SerializeField]
    private string[] lines;
    [SerializeField]
    private string currentLine;

    private GameManager gameManager;
    private ButtonClickSound buttonAudio;
    private AudioSource audioSource;
    private PlayerMovementController playerMovement;
    private Collider2D npcCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        buttonAudio = FindAnyObjectByType<ButtonClickSound>();
        audioSource = GetComponent<AudioSource>();
        playerMovement = FindAnyObjectByType<PlayerMovementController>();
        npcCollider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!playerMovement.inDialogue && dialogueIcon.activeInHierarchy)
            if (Input.GetKeyDown(KeyCode.Space))
                EnterDialogueState();
        
        if (playerMovement.inDialogue)
            if (Input.GetKeyDown(KeyCode.Space))
                PlayNextLine();
    }

    private void EnterDialogueState()
    {
        npcCollider2D.enabled = false;
        playerMovement.inDialogue = true;
        playerMovement.SetVelocityToZero();
    }

    private void PlayNextLine()
    {
        if (lineNum < lines.Length)
        {
            dialoguePanel.SetActive(true);
            npcDialogue.AnimateText(lines[lineNum]);
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
        npcCollider2D.enabled = true;
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
