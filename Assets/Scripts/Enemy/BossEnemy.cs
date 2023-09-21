using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EBossState
{
    Idle,
    Walk,
    Attack,
    Hit
}

public class BossEnemy : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private int lives = 3;
    [SerializeField] private float delayTime = 2f;
    [SerializeField] private float attackRange = 2f;

    private float delayTimer = 0f;
    private bool isDelaying;
    private EBossState currentState;

    private NavMeshAgent navAgent;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = velocity;

        currentState = EBossState.Idle;
        isDelaying = false;
    }

    private void Update()
    {
        float distanceFromPlayer = 0;
        Vector3 playerPosition = Vector3.zero;
        if (PlayerManager._playerPosition != null)
        {
            playerPosition = PlayerManager._playerPosition();
            distanceFromPlayer = Vector3.Distance(playerPosition, transform.position);
        }

        switch (currentState) 
        {
            case EBossState.Idle:
                //Start Idle anim
                if (distanceFromPlayer <= attackRange)
                {
                    currentState = EBossState.Attack;
                }
                break;

            case EBossState.Walk:
                //walk anim
                navAgent.SetDestination(playerPosition);
                if (distanceFromPlayer <= attackRange)
                {
                    currentState = EBossState.Attack;
                    navAgent.isStopped = true;
                }
                break;

            case EBossState.Attack:
                if(isDelaying == false)
                {                    
                    //attack animation
                    delayTimer = 0;
                    isDelaying = true;                    
                }

                if (isDelaying)
                {
                    delayTimer += Time.deltaTime;
                    if(delayTimer > delayTime)
                    {
                        isDelaying = false;
                        currentState = EBossState.Attack;
                        navAgent.isStopped = false;
                    }
                }
                break;
        }

    }

    private void StartDieAnimation()
    {
        //StarDieAnimation
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovementComponent>())
        {
            lives--;
        }

        if(lives <= 0)
        {
            StartDieAnimation();            
        }
    }
}
