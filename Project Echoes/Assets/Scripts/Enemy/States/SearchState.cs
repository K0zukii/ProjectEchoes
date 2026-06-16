using UnityEngine;

public class SearchState : IState
{
    private EnemyStateMachine _enemyState;
    private EnemyNavigation _enemyNavigation;
    private EnemyDetection _enemyDetection;
    
    private float timeBeforePatrol = 8;
    public SearchState(EnemyStateMachine enemyStateMachine, EnemyNavigation enemyNavigation, EnemyDetection enemyDetection)
    {
        _enemyState = enemyStateMachine;
        _enemyNavigation = enemyNavigation;
        _enemyDetection = enemyDetection;
    }

    public void OnEnterState()
    {
        Debug.Log("L'IA A COMMENCE LE SEARCH STATE !");
        _enemyNavigation.SetSpeed(2.5f);
        _enemyNavigation.StopMoving();
    }

    public void UpdateState()
    {
        if (_enemyDetection.HasHeardNoise)
        {
            _enemyState.ChangeState(new InvestigateState(_enemyState, _enemyNavigation, _enemyDetection));
        }
        else if (_enemyDetection.HasSeenPlayer)
        {
            _enemyState.ChangeState(new ChaseState(_enemyState, _enemyNavigation, _enemyDetection));
        }
        else
        {
            timeBeforePatrol -= Time.deltaTime;
            if (timeBeforePatrol <= 0)
            {
                _enemyState.ChangeState(new PatrolState(_enemyState, _enemyNavigation, _enemyDetection));
                timeBeforePatrol = 5;
            }
            else if(_enemyNavigation.HasReachedDestination())
            {
                _enemyNavigation.MoveToPosition(_enemyNavigation.GetRandomDestination(_enemyDetection.LastKnownPosition, 4f));
            }
        }
    }

    public void ExitState()
    {
        _enemyDetection.ResetNoiseAlert();
    }
}
