using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerStamina))]
[RequireComponent(typeof(NoiseEmitter))]
public class PlayerStateMachine : MonoBehaviour
{
    private IState _currentState;
    private PlayerMovement playerMovement;
    private PlayerInputHandler playerInput;
    private PlayerStamina playerStamina;
    private NoiseEmitter noiseEmitter;
    
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
        playerStamina = GetComponent<PlayerStamina>();
        noiseEmitter = GetComponent<NoiseEmitter>();
        
        ChangeState(new WalkState(this, playerMovement, playerInput, playerStamina, noiseEmitter));
    }

    void Update()
    {
        _currentState.UpdateState();
    }
}
