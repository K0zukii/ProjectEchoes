using UnityEngine;

public class SprintState : IState
{
    private PlayerStateMachine playerState;
    private PlayerMovement playerMovement;
    private PlayerInputHandler playerInput;
    private PlayerStamina playerStamina;

    public SprintState(PlayerStateMachine stateMachine, PlayerMovement movement, PlayerInputHandler input, PlayerStamina stamina)
    {
        playerState = stateMachine;
        playerMovement = movement;
        playerInput = input;
        playerStamina = stamina;
    }
    public void OnEnterState()
    {
        Debug.Log("Etat Actif : Course");
    }
    public void UpdateState()
    {
        playerMovement.Move(playerInput.MoveInput, 6f);
        playerStamina.ConsumeStamina();

        if(playerInput.IsSprinting == false || playerStamina.canSprint == false)
        {
            if (playerInput.IsCrouching)
            {
                playerState.ChangeState(new CrouchState(playerState, playerMovement, playerInput, playerStamina));
            }
            else
            {
                playerState.ChangeState(new WalkState(playerState, playerMovement, playerInput, playerStamina));
            }
        }
    }
    public void ExitState()
    {
        
    }
}
