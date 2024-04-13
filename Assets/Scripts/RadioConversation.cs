using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioConversation : MonoBehaviour
{
    private NPC nPC;

    // Start is called before the first frame update
    void Start()
    {
        nPC = GetComponent<NPC>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        nPC.EnterDialogueState();
        nPC.PlayNextLine();
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
