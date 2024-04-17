using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderHubIntro : MonoBehaviour
{
    [SerializeField]
    private string[] progressLines;

    private NPC nPC;

    private void Awake()
    {
        nPC = GetComponent<NPC>();
        
        if (ProgressManager.Level1Complete)
        {
            nPC.lines = new string[] { progressLines[0] };
        }
        if (ProgressManager.Level2Complete)
        {
            nPC.lines = new string[] { progressLines[1] };
        }
    }
}
