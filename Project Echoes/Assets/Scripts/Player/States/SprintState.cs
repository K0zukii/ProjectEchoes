using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SprintState : IState
{
    private PlayerStateMachine playerState;
    private PlayerMovement playerMovement;
    private PlayerInputHandler playerInput;
    private PlayerStamina playerStamina;
    private NoiseEmitter noiseEmitter;
    private float timer = 0.3f;

    public SprintState(PlayerStateMachine stateMachine, PlayerMovement movement, PlayerInputHandler input, PlayerStamina stamina, NoiseEmitter noise)
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
        playerMovement.Move(playerInput.MoveInput, 6f);
        playerStamina.ConsumeStamina();

        if (playerInput.MoveInput != Vector2.zero)
        {
            timer -= Time.deltaTime;
            noiseEmitter.PlaySound(true, 1.15f);
            if (timer <= 0)
            {
                noiseEmitter.EmitNoise(15f);
                timer = 0.3f;
            }
        }
        else
        {
            noiseEmitter.PlaySound(false, 1.15f);
        }


        if (playerInput.IsSprinting == false || playerStamina.CanSprint == false)
        {
            if (playerInput.IsCrouching)
            {
                playerState.ChangeState(new CrouchState(playerState, playerMovement, playerInput, playerStamina, noiseEmitter));
            }
            else
            {
                playerState.ChangeState(new WalkState(playerState, playerMovement, playerInput, playerStamina, noiseEmitter));
            }
        }
    }
    public void ExitState()
    {

    }
}
