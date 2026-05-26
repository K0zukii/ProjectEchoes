using UnityEngine;

public class SprintState : IState
{
    private PlayerStateMachine playerState;
    private PlayerMovement playerMovement;
    private PlayerInputHandler playerInput;

    public SprintState(PlayerStateMachine stateMachine, PlayerMovement movement, PlayerInputHandler input)
    {
        playerState = stateMachine;
        playerMovement = movement;
        playerInput = input;
    }
    public void OnEnterState()
    {
        Debug.Log("Etat Actif : Course");
    }
    public void UpdateState()
    {
        playerMovement.Move(playerInput.MoveInput, 6f);

        if(playerInput.IsSprinting == false )
        {
            if (playerInput.IsCrouching)
            {
                playerState.ChangeState(new CrouchState(playerState, playerMovement, playerInput));
            }
            else if (playerInput.MoveInput != null)
            {
                playerState.ChangeState(new WalkState(playerState, playerMovement, playerInput));
            }
        }
    }
    public void ExitState()
    {
        
    }
}
