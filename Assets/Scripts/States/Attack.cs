using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    public Attack(GameObject _npc, Animator _animator, NavMeshAgent _agent, Transform _player) : base(_npc, _animator, _agent, _player)
    {
        name = STATE.Attack;
        shoot = npc.GetComponent<AudioSource>();
    }

    private float rotationSpeed = 2.0f;
    private AudioSource shoot;

    public override void Enter()
    {
        animator.SetTrigger("isShooting");
        agent.isStopped = true;
        shoot.Play();
        base.Enter();
    }

    public override void Update()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        direction.y = 0;

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                        Time.deltaTime * rotationSpeed);

        if (!CanAttackPlayer())
        {
            nextState = new Idle(npc, animator, agent, player);
            stage = EVENT.Exit;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("isShooting");
        shoot.Stop();
        base.Exit();
    }
}
