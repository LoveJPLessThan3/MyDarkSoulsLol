using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyAnimator))]
public class AgentEnemyAnimation : MonoBehaviour
{
    public NavMeshAgent Agent;
    public EnemyAnimator Animator;
    private const float MinimalVelocity = 0.1f;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<EnemyAnimator>();
    }
    private void Update()
    {
        //если есть скорость, перевод
        if(MustMove())
        Animator.Move(Agent.velocity.magnitude);
        else
        {
            Animator.StopMove();
        }
    }

    private bool MustMove() => 
        Agent.velocity.magnitude > MinimalVelocity && Agent.remainingDistance > Agent.radius;
}
