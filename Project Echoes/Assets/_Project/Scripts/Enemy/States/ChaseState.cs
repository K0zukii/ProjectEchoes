using UnityEngine;

public class ChaseState : IState
{
    private EnemyStateMachine _enemyState;
    private EnemyNavigation _enemyNavigation;
    private EnemyDetection _enemyDetection;

    private readonly float _screamDuration = 2.24f;
    private float _screamTimer;
    private bool _hasFinishedScream;
    private bool _hasCaughtPlayer;
    private const float CatchDistance = 2.0f;

    public ChaseState(EnemyStateMachine enemyStateMachine, EnemyNavigation enemyNavigation, EnemyDetection enemyDetection)
    {
        _enemyState = enemyStateMachine;
        _enemyNavigation = enemyNavigation;
        _enemyDetection = enemyDetection;
    }

    public void OnEnterState()
    {
        _enemyState.AnimationController.TriggerRoar();

        _enemyNavigation.StopMoving();
        _screamTimer = _screamDuration;
        _hasFinishedScream = false;
        GameEvents.FireOnMonsterChaseStart();
    }

    public void UpdateState()
    {
        if (_hasCaughtPlayer) return;

        if (!_hasFinishedScream)
        {
            _screamTimer -= Time.deltaTime;

            if (_enemyDetection.PlayerTarget != null)
            {
                Vector3 lookTarget = _enemyDetection.PlayerTarget.position;
                lookTarget.y = _enemyState.transform.position.y;
                _enemyState.transform.LookAt(lookTarget);
            }

            if (_screamTimer <= 0f)
            {
                _hasFinishedScream = true;
                _enemyNavigation.SetSpeed(7f);
                _enemyDetection.RefreshLoseTrackTimer(6f);

                if (_enemyDetection.PlayerTarget != null)
                {
                    _enemyNavigation.MoveToPosition(_enemyDetection.PlayerTarget.position);
                }
            }

            return;
        }

        if (!_enemyDetection.HasSeenPlayer || _enemyDetection.PlayerTarget == null)
        {
            _enemyState.ChangeState(new SearchState(_enemyState, _enemyNavigation, _enemyDetection));
            return;
        }

        Vector3 enemyPosFlat = _enemyState.transform.position;
        Vector3 playerPosFlat = _enemyDetection.PlayerTarget.position;
        enemyPosFlat.y = 0f;
        playerPosFlat.y = 0f;

        float distanceToPlayer = Vector3.Distance(enemyPosFlat, playerPosFlat);

        if (distanceToPlayer <= CatchDistance)
        {
            _hasCaughtPlayer = true;
            _enemyNavigation.StopMoving();
            _enemyState.AnimationController.TriggerAttack();

            GameEvents.FireOnMonsterChaseEnd();
            GameEvents.FireOnPlayerCaughtByMonster(_enemyNavigation.transform);
            return;
        }

        _enemyNavigation.MoveToPosition(_enemyDetection.PlayerTarget.position);
    }

    public void ExitState()
    {
        _enemyDetection.ResetVisualAlert();
        GameEvents.FireOnMonsterChaseEnd();
    }
}
