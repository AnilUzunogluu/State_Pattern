using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : State
{
    public Chase(GameObject _npc, Animator _animator, NavMeshAgent _agent, Transform _player) : base(_npc, _animator, _agent, _player)
    {
        name = STATE.Chase;
        agent.speed = 5f;
        agent.isStopped = false;
    }


    public override void Enter()
    {
        animator.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);
        if (agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                nextState = new Attack(npc, animator, agent, player);
                stage = EVENT.Exit;
            }else if (!CanSeePlayer())
            {
                nextState = new Patrol(npc, animator, agent, player);
                stage = EVENT.Exit;
            }
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("isRunning");
        base.Exit();
    }
}
