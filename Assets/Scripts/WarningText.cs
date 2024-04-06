using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningText : MonoBehaviour
{
    public float disableDelay;
    public float disableSpeed;
    public string fullText;
    private string currentText = "";
    private TextMeshProUGUI textMesh;

    private AudioSource audioSource;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();

        fullText = textMesh.text;
        textMesh.text = "";

        textMesh.enabled = false;
        AnimateWarningText();
    }

    public void AnimateWarningText()
    {
        textMesh.enabled = true;
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
            textMesh.color.a - (disableSpeed * Time.deltaTime));
        yield return new WaitForSeconds(disableDelay);
        textMesh.enabled = false;
    }
}
