using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public bool isGameStarting;

    [SerializeField]
    ButtonClickSound buttonClickSound;

    public void StartGame()
    {
        isGameStarting = true;
        buttonClickSound.PlayFloppySound();
        StartCoroutine(LoadLevel("Hub"));
    }

    public IEnumerator LoadLevel(string sceneName)
    {
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene(sceneName);
    }
}
