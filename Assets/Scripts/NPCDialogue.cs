using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public float textSpeed;
    public string fullText;
 
    [SerializeField]
    private TextMeshProUGUI textMesh;

    private string currentText = "";
    private AudioSource audioSource;

    public void AnimateText(string text)
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();

        //fullText = warningText;
        textMesh.text = "";


        fullText = text;

        StopAllCoroutines();
        textMesh.enabled = true;
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1);

        StartCoroutine(ShowText());
    }

    public void ClearLines()
    {
        textMesh.text = "";
    }

    private IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            GetComponent<TMPro.TMP_Text>().text = currentText;
            audioSource.PlayOneShot(audioSource.clip);
            //float randFloat = Random.Range(0.1f, 0.25f);
            yield return new WaitForSeconds(textSpeed);
            //DisableWarningText();
        }
    }
}
