using UnityEngine;

public class WalkState : IState
{
    private PlayerStateMachine playerState;
    private PlayerMovement playerMovement;
    private PlayerInputHandler playerInput;
    private PlayerStamina playerStamina;
    private NoiseEmitter noiseEmitter;
    private float timer = 0.6f;

    public WalkState(PlayerStateMachine stateMachine, PlayerMovement movement, PlayerInputHandler input, PlayerStamina stamina, NoiseEmitter noise)
    {
        playerState = stateMachine;
        playerMovement = movement;
        playerInput = input;
        playerStamina = stamina;
        noiseEmitter = noise;
    }

    public void OnEnterState()
    {

    }
    public void UpdateState()
    {
        playerMovement.Move(playerInput.MoveInput, 3.5f);
        playerStamina.RegenerateStamina();

        if (playerInput.MoveInput != Vector2.zero)
        {
            timer -= Time.deltaTime;
            noiseEmitter.PlaySound(true, 1);
            if (timer <= 0)
            {
                noiseEmitter.EmitNoise(10f);
                timer = 0.6f;
            }
        }
        else
        {
            noiseEmitter.PlaySound(false, 1);
        }

        if (playerInput.IsSprinting && playerStamina.CanSprint)
        {
            playerState.ChangeState(new SprintState(playerState, playerMovement, playerInput, playerStamina, noiseEmitter));
        }
        else if (playerInput.IsCrouching)
        {
            playerState.ChangeState(new CrouchState(playerState, playerMovement, playerInput, playerStamina, noiseEmitter));
        }
    }
    public void ExitState() { }
}