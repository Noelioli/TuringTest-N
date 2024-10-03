using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    NavMeshAgent _agent;
    Vector3 _destination;

    public MoveCommand(NavMeshAgent agent, Vector3 destination)
    {
        _agent = agent;
        _destination = destination;
    }

    public override void Execute()
    {
        _agent.destination = _destination;
    }

    public override bool _isComplete => ReachedDestination();

    private bool ReachedDestination()
    {
        if (_agent.remainingDistance > 0.1f)
        {
            return false;
        }
        return true;
    }
}
