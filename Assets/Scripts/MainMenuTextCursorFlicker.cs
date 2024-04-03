using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuTextCursorFlicker : MonoBehaviour
{
    public TMP_Text text;
    private string newText;
    private string oldText;

    private bool isRoutineRunning = false;

    void Start()
    {
        oldText = "запуск системы...\nсистема готова\nс:\\Пользователи\\Система >";
        newText = "запуск системы...\nсистема готова\nс:\\Пользователи\\Система > II";
        text.text = oldText;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRoutineRunning)
        {
            StartCoroutine(SetText());
        }
    }

    private IEnumerator SetText()
    {
        isRoutineRunning = true;
        yield return new WaitForSeconds(1f);
        if (text.text == oldText)
        {
            text.text = newText;
        }
        else
        {
            text.text = oldText;
        }
        isRoutineRunning = false;
    }
}
