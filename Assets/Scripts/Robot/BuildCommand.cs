using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildCommand : Command
{
    NavMeshAgent _agent;
    Builder _builder;

    public BuildCommand(NavMeshAgent agent, Builder builder)
    {
        _agent = agent;
        _builder = builder;
    }

    public override void Execute()
    {
        _agent.SetDestination(_builder.transform.position);
    }

    public override bool _isComplete => BuildComplete();

    bool BuildComplete()
    {
        if (_agent.remainingDistance > 0.1f)
            return false;

        if (_builder != null)
            _builder.Build();

        return true;
    }
}
