using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainBossController : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed;
    [SerializeField]
    private GameObject[] joints;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private bool bossConfigSet = false;

    private EnemyMovementController EnemyMovementController;

    // Start is called before the first frame update
    void Start()
    {
        EnemyMovementController = GetComponent<EnemyMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        SetBossConfig();
        RotateJointsAntiClockwise();
    }

    private void SetBossConfig()
    {
        if (!bossConfigSet && EnemyMovementController.currentMoveState == EnemyMovementController.MoveStates.chasing)
        {
            audioSource.Play();
            Camera.main.orthographicSize = 10;
            bossConfigSet = true;
        }
    }

    private void RotateJointsAntiClockwise()
    {
        foreach (var joint in joints)
        {
            joint.transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
        }
    }
}
