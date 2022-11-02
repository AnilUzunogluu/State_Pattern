using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    public Patrol(GameObject _npc, Animator _animator, NavMeshAgent _agent, Transform _player) : base(_npc, _animator, _agent, _player)
    {
        name = STATE.Patrol;
            agent.speed = 2;
            agent.isStopped = false;
    }

    private int currentIndex = -1;

    public override void Enter()
    {
        float lastClosestDistance = Mathf.Infinity;

        for (int i = 0; i < GameEnvironment.Instance.CheckPoints.Count; i++)
        {
            var currentWayPoint = GameEnvironment.Instance.CheckPoints[i];
            var distance = Vector3.Distance(npc.transform.position, currentWayPoint.transform.position);
            
            if ( distance < lastClosestDistance)
            {
                currentIndex = i - 1;
                lastClosestDistance = distance;
            }
        }
        animator.SetTrigger("isWalking");
        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            if (currentIndex >= GameEnvironment.Instance.CheckPoints.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }

            agent.SetDestination(GameEnvironment.Instance.CheckPoints[currentIndex].transform.position);
        }
        
        if (CanSeePlayer())
        {
            nextState = new Chase(npc, animator, agent, player);
            stage = EVENT.Exit;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("isWalking");
        base.Exit();
    }
}
