using UnityEngine;

public class WalkState : IState
{
    private PlayerStateMachine playerState;
    private PlayerMovement playerMovement;
    private PlayerInputHandler playerInput;
    private PlayerStamina playerStamina;

    public WalkState(PlayerStateMachine stateMachine, PlayerMovement movement, PlayerInputHandler input, PlayerStamina stamina)
    {
        playerState = stateMachine;
        playerMovement = movement;
        playerInput = input;
        playerStamina = stamina;
    }

    public void OnEnterState()
    {
        Debug.Log("Etat Actif : Marche");
    }
    public void UpdateState()
    {
        playerMovement.Move(playerInput.MoveInput, 3.5f);
        playerStamina.RegenerateStamina();

        if (playerInput.IsSprinting && playerStamina.CanSprint)
        {
            playerState.ChangeState(new SprintState(playerState, playerMovement, playerInput, playerStamina));
        }
        else if (playerInput.IsCrouching)
        {
            playerState.ChangeState(new CrouchState(playerState, playerMovement, playerInput, playerStamina));
        }
    }
    public void ExitState() {}
}
