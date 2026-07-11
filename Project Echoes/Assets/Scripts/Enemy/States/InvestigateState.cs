using System;
using UnityEngine;

public class InvestigateState : IState
{
    private EnemyStateMachine _enemyState;
    private EnemyNavigation _enemyNavigation;
    private EnemyDetection _enemyDetection;
    private Vector3 lastKnowPos;

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
        lastKnowPos = _enemyDetection.LastKnownPosition;
        _enemyNavigation.MoveToPosition(lastKnowPos);
        _enemyDetection.IsInvesting = true;
    }

    public void UpdateState()
    {
        if (_enemyDetection.HasSeenPlayer)
        {
            _enemyState.ChangeState(new ChaseState(_enemyState, _enemyNavigation, _enemyDetection));
        }
        else if (_enemyNavigation.HasReachedDestination())
        {
            _enemyState.ChangeState(new SearchState(_enemyState, _enemyNavigation, _enemyDetection));
        }
    }

    public void ExitState()
    {
        _enemyDetection.IsInvesting = false;
        _enemyDetection.ResetNoiseAlert();
    }
}
