using UnityEngine;

public class PatrolState : IState
{
    private EnemyStateMachine _enemyState;
    private EnemyNavigation _enemyNavigation;
    private EnemyDetection _enemyDetection;

    private float timer = 3f;
    public PatrolState(EnemyStateMachine enemyStateMachine, EnemyNavigation enemyNavigation, EnemyDetection enemyDetection)
    {
        _enemyState = enemyStateMachine;
        _enemyNavigation = enemyNavigation;
        _enemyDetection = enemyDetection;
    }

    public void OnEnterState()
    {
        Debug.Log("L'IA A COMMENCE LE PATROL STATE !");
        _enemyNavigation.SetSpeed(3.5f);
        _enemyNavigation.MoveToPosition(_enemyNavigation.GetRandomDestination(_enemyState.gameObject.transform.position, 15f));
    }

    public void UpdateState()
    {
        if (_enemyDetection.HasSeenPlayer)
        {
            _enemyState.ChangeState(new ChaseState(_enemyState, _enemyNavigation, _enemyDetection));
        }
        else if (_enemyDetection.HasHeardNoise)
        {
            _enemyState.ChangeState(new InvestigateState(_enemyState, _enemyNavigation, _enemyDetection));
        }
        else
        {
            if (!_enemyNavigation.HasReachedDestination())
            {
                return;
            }
            else
            {
                _enemyNavigation.StopMoving();
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    _enemyNavigation.MoveToPosition(_enemyNavigation.GetRandomDestination(_enemyState.gameObject.transform.position, 15f));
                    timer = 3;
                }
            }
        }
    }

    public void ExitState()
    {

    }
}
