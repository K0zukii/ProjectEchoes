using UnityEngine;

public class CrouchState : IState
{
    private PlayerStateMachine playerState;
    private PlayerMovement playerMovement;
    private PlayerInputHandler playerInput;
    private PlayerStamina playerStamina;
    private NoiseEmitter noiseEmitter;
    private float timer = 0.8f;

    public CrouchState(PlayerStateMachine stateMachine, PlayerMovement movement, PlayerInputHandler input, PlayerStamina stamina, NoiseEmitter noise)
    {
        playerState = stateMachine;
        playerMovement = movement;
        playerInput = input;
        playerStamina = stamina;
        noiseEmitter = noise;
    }

    public void OnEnterState()
    {
        playerMovement.SetCrouchHeight();
    }
    public void UpdateState()
    {
        playerMovement.Move(playerInput.MoveInput, 1.5f);
        playerStamina.RegenerateStamina();

        if (playerInput.MoveInput != Vector2.zero)
        {
            timer -= Time.deltaTime;
            noiseEmitter.PlaySound(true, 0.85f);
            if(timer <= 0)
            {
                noiseEmitter.EmitNoise(2f);
                timer = 0.8f;
            }
        }
        else
        {
            noiseEmitter.PlaySound(false, 0.85f);
        }
        
        if (playerInput.IsCrouching == false)
        {
            if (playerInput.IsSprinting)
            {
                playerState.ChangeState(new SprintState(playerState, playerMovement, playerInput, playerStamina, noiseEmitter));
            }
            else
            {
                playerState.ChangeState(new WalkState(playerState, playerMovement, playerInput, playerStamina, noiseEmitter));
            }
        }
    }
    public void ExitState()
    {
        playerMovement.SetStandHeight();
    }
}