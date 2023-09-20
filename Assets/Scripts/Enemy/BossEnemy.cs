using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EBossState
{
    walking,
    attacking,
    hit
}

public class BossEnemy : MonoBehaviour
{
    [SerializeField] private int lifes;

    private EBossState bossState;
    private Animator animator;
    private NavMeshAgent navAgent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }
    
    
}
