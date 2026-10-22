using UnityEngine;

public class ExitRoomState : IState
{
    private EnemyStateMachine _enemyState;
    private EnemyNavigation _enemyNavigation;
    private EnemyDetection _enemyDetection;

    private readonly Vector3 _targetCenter = new(-2.4840467f, 0.100000024f, -35.3687172f);

    public ExitRoomState(EnemyStateMachine enemyStateMachine, EnemyNavigation enemyNavigation, EnemyDetection enemyDetection)
    {
        _enemyState = enemyStateMachine;
        _enemyNavigation = enemyNavigation;
        _enemyDetection = enemyDetection;
    }

    public void OnEnterState()
    {
        _enemyNavigation.SetSpeed(3.5f);
        _enemyNavigation.MoveToPosition(_targetCenter);
    }

    public void UpdateState()
    {
        if (_enemyDetection.HasSeenPlayer)
        {
            _enemyState.ChangeState(new ChaseState(_enemyState, _enemyNavigation, _enemyDetection));
            return;
        }

        if (_enemyNavigation.HasReachedDestination())
        {
            _enemyState.ChangeState(new SearchState(_enemyState, _enemyNavigation, _enemyDetection));
        }
    }

    public void ExitState()
    {
        _enemyNavigation.StopMoving();
    }
}
