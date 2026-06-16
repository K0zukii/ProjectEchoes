using UnityEngine;

[RequireComponent(typeof(EnemyNavigation))]
[RequireComponent(typeof(EnemyDetection))]
public class EnemyStateMachine : MonoBehaviour
{
    private IState _currentState;
    private EnemyNavigation enemyMovement;
    private EnemyDetection enemyDetection;

    public void ChangeState(IState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        newState.OnEnterState();
    }

    void Start()
    {
        enemyMovement = GetComponent<EnemyNavigation>();
        enemyDetection = GetComponent<EnemyDetection>();
        
        ChangeState(new SleepState(this, enemyMovement, enemyDetection));
    }


    void Update()
    {
        _currentState.UpdateState();
    }
}
