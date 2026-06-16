using UnityEngine;

public class ChaseState : IState
{
    private EnemyStateMachine _enemyState;
    private EnemyNavigation _enemyNavigation;
    private EnemyDetection _enemyDetection;
    
    public ChaseState(EnemyStateMachine enemyStateMachine, EnemyNavigation enemyNavigation, EnemyDetection enemyDetection)
    {
        _enemyState = enemyStateMachine;
        _enemyNavigation = enemyNavigation;
        _enemyDetection = enemyDetection;
    }

    public void OnEnterState()
    {
        Debug.Log("L'IA A COMMENCE LE CHASE STATE !");
        _enemyNavigation.SetSpeed(7f);
        _enemyNavigation.MoveToPosition(_enemyDetection.PlayerTarget.position);
    }

    public void UpdateState()
    {
        if (_enemyDetection.HasSeenPlayer == false)
        {
            _enemyState.ChangeState(new SearchState(_enemyState, _enemyNavigation, _enemyDetection));
        }
        else if (_enemyNavigation.HasReachedDestination())
        {
            _enemyNavigation.StopMoving();
            GameEvents.FireOnPlayerCaught();
        }
        else
        {
            _enemyNavigation.MoveToPosition(_enemyDetection.PlayerTarget.position);
        }
    }

    public void ExitState()
    {
        _enemyDetection.ResetVisualAlert();
    }
}
