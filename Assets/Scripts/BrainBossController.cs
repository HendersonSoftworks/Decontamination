using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainBossController : MonoBehaviour
{
    public int jointCount;
    public Slider bossHealthSlider;
    public float health = 0;
    public bool bossIsDefeated = false;
    public NPC defeatedNPC;
    //public GameObject fadePanel;
    //public float fadeSpeed;

    [SerializeField]
    private float rotSpeed;
    [SerializeField]
    public GameObject[] joints;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip bossClip;
    [SerializeField]
    private bool bossConfigSet = false;
    
    private EnemyMovementController EnemyMovementController;
    private EnemyHealthManager healthManager;
    private ZombieAttackController zombieAttack;

    // Start is called before the first frame update
    void Start()
    {
        EnemyMovementController = GetComponent<EnemyMovementController>();
        healthManager = GetComponent<EnemyHealthManager>();
        zombieAttack = GetComponent<ZombieAttackController>();

        jointCount = joints.Length;
        bossHealthSlider.gameObject.SetActive(false);

        foreach (var joint in joints)
        {
            health += joint.GetComponent<BossJoint>().health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !bossIsDefeated)
        {
            bossIsDefeated = true;
            defeatedNPC.transform.position = transform.position;
            defeatedNPC.GetComponent<SpriteRenderer>().enabled = true;
            audioSource.Stop();
            healthManager.Die();
        }

        bossHealthSlider.value = health;

        SetBossConfig();
        RotateJointsAntiClockwise();
    }

    private void SetBossConfig()
    {
        if (!bossConfigSet && EnemyMovementController.currentMoveState == EnemyMovementController.MoveStates.chasing)
        {
            GameManager.currentNPC = defeatedNPC;
            audioSource.clip = bossClip;
            audioSource.Play();
            Camera.main.orthographicSize = 10;
            bossHealthSlider.gameObject.SetActive(true);
            bossConfigSet = true;
        }
    }

    private void RotateJointsAntiClockwise()
    {
        foreach (var joint in joints)
        {
            if (joint == null)
            {
                return;
            }

            joint.transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            zombieAttack.AttackPlayer();            
        }
    }
}
