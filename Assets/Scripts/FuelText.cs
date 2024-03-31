using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelText : MonoBehaviour
{
    [SerializeField]
    float timer;

    private void Start()
    {
        StartCoroutine(AnimateSpin());
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * 360, 0));
        transform.Translate(Vector2.up * Time.deltaTime);
    }

    private IEnumerator AnimateSpin()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
