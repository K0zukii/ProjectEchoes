using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform playerCam;
    [SerializeField] private PlayerInputHandler playerInput;
    [SerializeField] private float sensitivity;
    private float xRotation = 0f;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseVelocity;
    [SerializeField] private float smoothTime = 0.05f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        CalculateLook();
    }

    void CalculateLook()
    {
        Vector2 targetMouseDelta = playerInput.LookInput;

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseVelocity, smoothTime);
    
        float mouseX = currentMouseDelta.x;
        float mouseY = currentMouseDelta.y;

        mouseX *= sensitivity;
        mouseY *= sensitivity;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}
