using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    private NavMeshAgent _agent;
    private Animator _animator;
    private State _currentState;

    public Transform player;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _currentState = new Idle(gameObject, _animator, _agent, player);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState = _currentState.Execute();
    }
}
