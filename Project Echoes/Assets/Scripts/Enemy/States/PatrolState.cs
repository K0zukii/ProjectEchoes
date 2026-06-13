using UnityEngine;

public class PatrolState : IState
{
    private EnemyStateMachine _enemyState;
    private EnemyNavigation _enemyNavigation;
    public PatrolState(EnemyStateMachine enemyStateMachine, EnemyNavigation enemyNavigation)
    {
        _enemyState = enemyStateMachine;
        _enemyNavigation = enemyNavigation;
    }

    public void OnEnterState()
    {
        Debug.Log("L'IA EST EN PATROL STATE !");
    }

    public void UpdateState()
    {
        
    }

    public void ExitState()
    {
        Debug.Log("L'IA QUITTE LE PATROL STATE !");
    }
}
