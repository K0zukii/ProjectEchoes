using UnityEngine;

[RequireComponent(typeof(EnemyNavigation))]
public class EnemyStateMachine : MonoBehaviour
{
    private IState _currentState;
    private EnemyNavigation enemyMovement;

    public void ChangeState(IState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        newState.OnEnterState();
    }

    void Start()
    {
        enemyMovement = GetComponent<EnemyNavigation>();
        
        ChangeState(new PatrolState(this, enemyMovement));
    }


    void Update()
    {
        _currentState.UpdateState();
    }
}
