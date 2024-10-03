using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : EnemyState
{
    float _distanceToPlayer;

    public EnemyFollowState(EnemyController enemy) : base(enemy)
    {

    }

    public override void OnStateEnter()
    {
        Debug.Log("Enter Follow");
    }

    public override void OnStateExit()
    {
        Debug.Log("Exit Follow");
    }

    public override void OnStateUpdate()
    {
        if (_enemy._player != null)
        {
            _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);

            if (_distanceToPlayer > _enemy._playerCheckDistance)
            {
                _enemy.ChangeState(new EnemyIdleState(_enemy));
            }

            if (_distanceToPlayer < _enemy._playerCheckDistance / 5)
            {
                _enemy.ChangeState(new EnemyAttackState(_enemy));
            }

            _enemy._agent.destination = _enemy._player.position;
        }
        else
        {
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }
}
