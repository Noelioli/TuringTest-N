using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyState _currentState;
    public Transform _enemy;
    public float _playerCheckDistance;
    public float _aggroDistance;
    public float _checkRadius = 0.4f;
    public Transform[] _targetPoints;

    public NavMeshAgent _agent;

    public Transform _player;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentState = new EnemyIdleState(this);
        _currentState.OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.OnStateUpdate();
    }

    public void ChangeState(EnemyState state)
    {
        _currentState.OnStateExit();
        _currentState = state;
        _currentState.OnStateEnter();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_enemy.position, _checkRadius);
        Gizmos.DrawWireSphere(_enemy.position + _enemy.forward * _playerCheckDistance, _checkRadius);

        Gizmos.DrawLine(_enemy.position, _enemy.position + _enemy.forward * _playerCheckDistance);
    }
}
