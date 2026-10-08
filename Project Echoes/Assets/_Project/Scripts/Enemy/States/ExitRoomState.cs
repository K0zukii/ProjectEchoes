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
        Debug.Log("<color=cyan> [IA] Sortie de la cellule vers le centre...</color>");
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

        _enemyNavigation.MoveToPosition(_targetCenter);

        Vector3 enemyPosFlat = _enemyState.transform.position;
        Vector3 targetFlat = _targetCenter;
        enemyPosFlat.y = 0f;
        targetFlat.y = 0f;

        if (Vector3.Distance(enemyPosFlat, targetFlat) <= 1.5f || _enemyNavigation.HasReachedDestination())
        {
            Debug.Log("<color=cyan>[IA] Centre atteint, début des rondes normales.</color>");
            _enemyState.ChangeState(new SearchState(_enemyState, _enemyNavigation, _enemyDetection));
        }
    }

    public void ExitState()
    {
        _enemyNavigation.StopMoving();
    }
}
