using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    [SerializeField]
    private NPC defeatedNPC;
    [SerializeField]
    private bool fadeStarted;
    [SerializeField]
    private GameObject fadePanel;
    
    void Start()
    {
        defeatedNPC = GetComponent<NPC>();
    }

    void Update()
    {
        if (defeatedNPC.lineNum == defeatedNPC.lines.Length && !fadeStarted)
        {
            fadeStarted = true;
            fadePanel.SetActive(true);
            StartCoroutine(DelayedLoadToMainMenu());
        }
    }

    public IEnumerator DelayedLoadToMainMenu()
    {
        var fadeImg = fadePanel.GetComponent<Image>();
        float startAlpha = fadeImg.color.a;

        float rate = 1.0f / 5;
        float progress = 0.0f;

        while (progress < 1.0)
        {
            Color tempColor = fadeImg.color;
            fadeImg.color = new Color(0, 0, 0, Mathf.Lerp(startAlpha, 1, progress));

            progress += rate * Time.deltaTime;

            yield return null;
        }

        fadeImg.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(10);

        SceneManager.LoadScene("Credits");
    }
}
