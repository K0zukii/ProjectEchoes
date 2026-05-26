using UnityEngine;

public class CrouchState : IState
{
    private PlayerStateMachine playerState;
    private PlayerMovement playerMovement;
    private PlayerInputHandler playerInput;
    private PlayerStamina playerStamina;

    public CrouchState(PlayerStateMachine stateMachine, PlayerMovement movement, PlayerInputHandler input, PlayerStamina stamina)
    {
        playerState = stateMachine;
        playerMovement = movement;
        playerInput = input;
        playerStamina = stamina;
    }
    public void OnEnterState()
    {
        playerMovement.SetCrouchHeight();
        Debug.Log("Etat Actif : Accroupis");
    }
    public void UpdateState()
    {
        playerMovement.Move(playerInput.MoveInput, 1.5f);
        playerStamina.RegenerateStamina();

        if (playerInput.IsCrouching == false)
        {
            if (playerInput.IsSprinting)
            {
                playerState.ChangeState(new SprintState(playerState, playerMovement, playerInput, playerStamina));
            }
            else
            {
                playerState.ChangeState(new WalkState(playerState, playerMovement, playerInput, playerStamina));
            }
        }
    }
    public void ExitState()
    {
        playerMovement.SetStandHeight();
    }
}
