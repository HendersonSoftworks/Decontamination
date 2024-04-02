using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject monster;
    [SerializeField]
    private int maxNumToSpawn;
    [SerializeField]
    private float spawnArea;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Start()
    {
        int numToSpawn = Random.Range(1, maxNumToSpawn+1);
        
        for (int i = 0; i < numToSpawn; i++)
        {
            float xPos = Random.Range(-spawnArea, spawnArea);
            float yPos = Random.Range(-spawnArea, spawnArea);

            Vector2 spawnPos = new Vector2(transform.position.x + xPos, transform.position.y + yPos);
            Instantiate(monster, spawnPos, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, spawnArea);
    }
}
