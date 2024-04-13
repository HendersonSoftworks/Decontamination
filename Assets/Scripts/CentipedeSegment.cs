using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeSegment : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private EnemyMovementController enemyMovement;

    void Start()
    {

    }

    void Update()
    {
        // Calculate the new position the segment should move to
        //Vector3 targetPosition = target.position + offset;
        Vector3 targetPosition = target.position;
        transform.LookAt(target);

        // Move the segment towards the target position
        //transform.position = Vector3.Lerp(transform.position, targetPosition, enemyMovement.speed * Time.deltaTime *2);
        //transform.position =  Vector3.MoveTowards(transform.position, targetPosition, enemyMovement.speed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 2 * Time.deltaTime);
    }
}
