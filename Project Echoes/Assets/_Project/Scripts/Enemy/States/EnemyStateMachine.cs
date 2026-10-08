using UnityEngine;
[RequireComponent(typeof(EnemyNavigation))]
[RequireComponent(typeof(EnemyDetection))]

[RequireComponent(typeof(EnemyAnimation))]
public class EnemyStateMachine : MonoBehaviour
{
    private IState _currentState;
    private EnemyNavigation enemyMovement;
    private EnemyDetection enemyDetection;
    public EnemyAnimation AnimationController { get; private set; }

    public void ChangeState(IState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        newState.OnEnterState();
    }

    void Start()
    {
        AnimationController = GetComponent<EnemyAnimation>();
        enemyMovement = GetComponent<EnemyNavigation>();
        enemyDetection = GetComponent<EnemyDetection>();

        ChangeState(new SleepState(this, enemyMovement, enemyDetection));
    }


    void Update()
    {
        _currentState.UpdateState();
    }
}
