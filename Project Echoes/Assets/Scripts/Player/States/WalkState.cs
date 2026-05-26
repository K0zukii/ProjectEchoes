using UnityEngine;

public class WalkState : IState
{
    private PlayerStateMachine playerState;
    private PlayerMovement playerMovement;
    private PlayerInputHandler playerInput;

    public WalkState(PlayerStateMachine stateMachine, PlayerMovement movement, PlayerInputHandler input)
    {
        playerState = stateMachine;
        playerMovement = movement;
        playerInput = input;
    }

    public void OnEnterState()
    {
        Debug.Log("Etat Actif : Marche");
    }
    public void UpdateState()
    {
        playerMovement.Move(playerInput.MoveInput, 3.5f);

        if (playerInput.IsSprinting)
        {
            playerState.ChangeState(new SprintState(playerState, playerMovement, playerInput));
        }
        else if (playerInput.IsCrouching)
        {
            playerState.ChangeState(new CrouchState(playerState, playerMovement, playerInput));
        }
    }
    public void ExitState() {}
}
