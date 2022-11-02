using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class State
{
    public enum STATE
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Sleep
    }

    public enum EVENT
    {
        Enter,
        Update,
        Exit
    }

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator animator;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;

    private float visibiltyDistance = 10f;
    private float visibiltyAngle = 30f;
    private float shootDistance = 7f;

    public State(GameObject _npc, Animator _animator, NavMeshAgent _agent, Transform _player)
    {
        npc = _npc;
        animator = _animator;
        agent = _agent;
        player = _player;
        stage = EVENT.Enter;
    }

    public virtual void Enter() { stage = EVENT.Update;}
    public virtual void Update() { stage = EVENT.Update; }
    public virtual void Exit() { stage = EVENT.Exit; }

    public State Execute()
    {
        if (stage == EVENT.Enter)
        {
            Enter();
        } 
        
        if (stage == EVENT.Update)
        {
            Update();
        } 
        
        if (stage == EVENT.Exit)
        {
            Exit();
            return nextState;
        }

        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if (direction.magnitude <= visibiltyDistance && angle <= visibiltyAngle)
        {
            return true;
        }

        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        if (direction.magnitude <= shootDistance)
        {
            return true;
        }

        return false;
    }
}
