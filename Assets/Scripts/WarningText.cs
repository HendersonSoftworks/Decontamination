using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningText : MonoBehaviour
{
    public float disableDelay;
    public float disableSpeed;
    public string fullText;
    private string warningText = "ALL CONTAMINANTS MUST BE CLEANSED ";
    private string successText= "ALL CONTAMINANTS CLEANSED - THIS IS ACCEPTABLE";

    private string currentText = "";
    private TextMeshProUGUI textMesh;

    private AudioSource audioSource;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();

        fullText = warningText;
        textMesh.text = "";

        textMesh.enabled = false;
        AnimateWarningText();
    }

    public void AnimateWarningText()
    {
        fullText = warningText;

        StopAllCoroutines();
        textMesh.enabled = true;
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1);

        StartCoroutine(ShowText());
    }

    // zzz
    public void AnimateSuccessText()
    {
        fullText = successText;

        StopAllCoroutines();
        textMesh.enabled = true;
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1);

        StartCoroutine(ShowText());
    }

    public void DisableWarningText()
    {
        StartCoroutine(DisableText());
    }

    private IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            GetComponent<TMPro.TMP_Text>().text = currentText;
            audioSource.PlayOneShot(audioSource.clip);
            float randFloat = Random.Range(0.1f, 0.25f);
            yield return new WaitForSeconds(randFloat);
            DisableWarningText();
        }
    }

    private IEnumerator DisableText()
    {
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 
            Mathf.Clamp(textMesh.color.a - (disableSpeed * Time.deltaTime ), 0.05f, 1));
        yield return new WaitForSeconds(disableDelay);
        textMesh.enabled = false;
    }
}
