using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float _distanceToPlayer;
    Health _playerHealth;
    float _damagePerSecond = 10f;

    public EnemyAttackState(EnemyController enemy) : base(enemy)
    {
        _playerHealth = enemy._player.GetComponent<Health>();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Enter Attack");
    }

    public override void OnStateExit()
    {
        Debug.Log("Exit Attack");
    }

    public override void OnStateUpdate()
    {
        Attack();

        if (_enemy._player != null)
        {
            _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);

            if (_distanceToPlayer > _enemy._playerCheckDistance / 5 )
            {
                _enemy.ChangeState(new EnemyFollowState(_enemy));
            }

            _enemy._agent.destination = _enemy._player.position;
        }
        else
        {
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }

    void Attack()
    {
        if (_playerHealth != null)
        {
            _playerHealth.DeductHealth(_damagePerSecond * Time.deltaTime);
        }
    }
}
