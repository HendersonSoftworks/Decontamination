using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningText : MonoBehaviour
{
    public float disableDelay;
    public float disableSpeed;
    public string fullText;
    public string warningText = "ALL CONTAMINANTS MUST BE CLEANSED";
    public string successText = "ALL CONTAMINANTS CLEANSED - THIS IS ACCEPTABLE";
    public string deathText = "DEATH IS NOT ACCEPTABLE. TRY AGAIN.";

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

    public void AnimateSuccessText()
    {
        fullText = successText;

        StopAllCoroutines();
        textMesh.enabled = true;
        textMesh.color = new Color(textMesh.color.r, 1, textMesh.color.b, 1);

        StartCoroutine(ShowText());
    }

    public void AnimateDeathText()
    {
        fullText = deathText;

        StopAllCoroutines();
        textMesh.enabled = true;
        textMesh.color = new Color(1, 1, textMesh.color.b, 1);

        StartCoroutine(ShowText());
    }


    public void DisableWarningText()
    {
        StartCoroutine(DisableText());
    }

    private IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length+1; i++)
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
