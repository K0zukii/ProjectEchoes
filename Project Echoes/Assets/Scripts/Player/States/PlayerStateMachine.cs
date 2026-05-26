using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerMovement))]

[RequireComponent(typeof(PlayerInput))]
public class PlayerStateMachine : MonoBehaviour
{
    private IState _currentState;
    private PlayerMovement playerMovement;
    private PlayerInputHandler playerInput;
    
    public void ChangeState(IState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        newState.OnEnterState();
    }
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInputHandler>();
        
        ChangeState(new WalkState(this, playerMovement, playerInput));
    }

    void Update()
    {
        _currentState.UpdateState();
    }
}
