using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    public Idle(GameObject _npc, Animator _animator, NavMeshAgent _agent, Transform _player) : base(_npc, _animator, _agent, _player)
    {
        name = STATE.Idle;
    }

    public override void Enter()
    {
        animator.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new Chase(npc, animator, agent, player);
            stage = EVENT.Exit;
        }
        else if (Random.Range(0f,1f)<0.01f)
        {
            nextState = new Patrol(npc, animator, agent, player);
            stage = EVENT.Exit;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("isIdle");
        base.Exit();
    }
}
