using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour
{
    private ZombieAttackController attackController;

    
    void Start()
    {
        attackController = GetComponentInParent<ZombieAttackController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackController.AttackPlayer();
        }
    }
}
