using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class test_moveTo : MonoBehaviour
{
    [SerializeField] private List<NavMeshAgent> _agent;
    [SerializeField] private Transform _pointToMove;

    private void Start()
    {
        foreach (NavMeshAgent agent in _agent)
        {
            agent.destination = _pointToMove.position;
        }
    }
}
