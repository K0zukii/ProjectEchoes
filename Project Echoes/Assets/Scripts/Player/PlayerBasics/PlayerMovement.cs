using System;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController charController;
    public float currentSpeed;
    [SerializeField] private float gravity = 19.62f;
    [SerializeField] private Vector3 velocity;

    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform playerCam;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        CalculateGravity();
    }

    public void Move(Vector2 input, float speed)
    {
        Vector3 direction = input.x * transform.right + input.y * transform.forward;
        charController.Move(speed * Time.deltaTime * direction);
    }

    public void SetCrouchHeight()
    {
        charController.height /= 2;
        charController.center = new Vector3(charController.center.x, -0.5f, charController.center.z);
        playerCam.localPosition = new Vector3(playerCam.localPosition.x, 0.4f, playerCam.localPosition.z);
    }

    public void SetStandHeight()
    {
        charController.height = 2f;
        charController.center = new Vector3(charController.center.x, 0, charController.center.z);
        playerCam.localPosition = new Vector3(playerCam.localPosition.x, 0.8f, playerCam.localPosition.z);
    }

    void CalculateGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, layerMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        velocity.y -= gravity * Time.deltaTime;
        charController.Move(velocity * Time.deltaTime);
    }
}
