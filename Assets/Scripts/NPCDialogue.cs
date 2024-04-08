using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public float textSpeed;
    public string fullText;
    public NPC currentNPC;

    [SerializeField]
    private TextMeshProUGUI textMesh;

    private string currentText = "";
    private AudioSource audioSource;


    public void AnimateText()
    {
        StopAllCoroutines();
        //ClearLines();

        textMesh = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
        fullText = currentNPC.lines[currentNPC.lineNum];

        textMesh.enabled = true;

        if (currentNPC.lineNum % 2 == 0)
        {
            audioSource.pitch = 0.9f;
            textMesh.color = Color.white;
            textMesh.alignment = TextAlignmentOptions.TopRight;
        }
        else
        {
            audioSource.pitch = 1;
            textMesh.color = Color.yellow;
            textMesh.alignment = TextAlignmentOptions.TopLeft;
        }

        StartCoroutine(ShowText());
    }

    public void ClearLines()
    {
        textMesh.text = "";
        currentText = "";
    }

    private IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            GetComponent<TMPro.TMP_Text>().text = currentText;
            audioSource.PlayOneShot(audioSource.clip);
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
