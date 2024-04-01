using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsStartup : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.enabled = false;
        }
    }
}
