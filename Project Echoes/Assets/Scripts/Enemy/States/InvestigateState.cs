using System;
using UnityEngine;

public class InvestigateState : IState
{
    private EnemyStateMachine _enemyState;
    private EnemyNavigation _enemyNavigation;
    private EnemyDetection _enemyDetection;
    
    public InvestigateState(EnemyStateMachine enemyStateMachine, EnemyNavigation enemyNavigation, EnemyDetection enemyDetection)
    {
        _enemyState = enemyStateMachine;
        _enemyNavigation = enemyNavigation;
        _enemyDetection = enemyDetection;
    }

    public void OnEnterState()
    {
        Debug.Log("L'IA A COMMENCE LE INVESTIGATE STATE !");
        _enemyNavigation.SetSpeed(4.5f);
        _enemyNavigation.MoveToPosition(_enemyDetection.LastKnownPosition);
    }

    public void UpdateState()
    {
        if (_enemyDetection.HasSeenPlayer)
        {
            _enemyState.ChangeState(new ChaseState(_enemyState, _enemyNavigation, _enemyDetection));
        }
        if (_enemyNavigation.HasReachedDestination())
        {
            _enemyState.ChangeState(new SearchState(_enemyState, _enemyNavigation, _enemyDetection));
        }
    }

    public void ExitState()
    {
        _enemyDetection.ResetNoiseAlert();
    }
}
