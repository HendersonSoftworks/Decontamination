using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    [SerializeField]
    private GameObject decal;

    public void SpawnDecal()
    {
        GameObject decalBloodSplatter = Instantiate(decal, transform.position, Quaternion.identity);
        Quaternion currentRot = decalBloodSplatter.transform.rotation;
        float randomZ = Random.Range(0f, 1f);
        Quaternion newRot = new Quaternion(currentRot.x, currentRot.y, randomZ, currentRot.w);
        decalBloodSplatter.transform.rotation = newRot;

        Destroy(gameObject);
    }
}
