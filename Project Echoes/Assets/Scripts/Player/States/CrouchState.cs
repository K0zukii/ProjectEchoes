using UnityEngine;

public class CrouchState : IState
{
    private PlayerStateMachine playerState;
    private PlayerMovement playerMovement;
    private PlayerInputHandler playerInput;

    public CrouchState(PlayerStateMachine stateMachine, PlayerMovement movement, PlayerInputHandler input)
    {
        playerState = stateMachine;
        playerMovement = movement;
        playerInput = input;
    }
    public void OnEnterState()
    {
        playerMovement.SetCrouchHeight();
        Debug.Log("Etat Actif : Accroupis");
    }
    public void UpdateState()
    {
        playerMovement.Move(playerInput.MoveInput, 1.5f);

        if (playerInput.IsCrouching == false)
        {
            if (playerInput.IsSprinting)
            {
                playerState.ChangeState(new SprintState(playerState, playerMovement, playerInput));
            }
            else if (playerInput.MoveInput != null)
            {
                playerState.ChangeState(new WalkState(playerState, playerMovement, playerInput));
            }
        }
    }
    public void ExitState()
    {
        playerMovement.SetStandHeight();
    }
}
