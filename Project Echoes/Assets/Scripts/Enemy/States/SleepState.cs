using UnityEngine;

public class SleepState : IState
{
    private EnemyStateMachine _enemyState;
    private EnemyNavigation _enemyNavigation;
    private EnemyDetection _enemyDetection;

    public SleepState(EnemyStateMachine enemyStateMachine, EnemyNavigation enemyNavigation, EnemyDetection enemyDetection)
    {
        _enemyState = enemyStateMachine;
        _enemyNavigation = enemyNavigation;
        _enemyDetection = enemyDetection;
    }

    public void OnEnterState()
    {
        _enemyDetection.isAsleep = true;
        _enemyNavigation.StopMoving();
    }

    public void UpdateState()
    {
        if (_enemyDetection.Gen2Activated)
        {
            _enemyState.ChangeState(new PatrolState(_enemyState, _enemyNavigation, _enemyDetection));
        }
    }

    public void ExitState()
    {
        _enemyDetection.isAsleep = false;
    }
}
