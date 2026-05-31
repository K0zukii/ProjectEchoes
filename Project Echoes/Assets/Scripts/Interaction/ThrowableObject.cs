using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrowableObject : MonoBehaviour, IInteractable
{
    private Rigidbody objectRb;
    [SerializeField] private Transform playerCam;
    [SerializeField] private PlayerInputHandler playerInput;

    private bool isHeld = false;

    void Start()
    {
        objectRb = GetComponent<Rigidbody>();

        playerInput.IsThrowing += ThrowObject;
    }

    void OnDisable()
    {
        playerInput.IsThrowing -= ThrowObject;
    }
    public void Interact()
    {
        if (isHeld) return;
        isHeld = true;

        objectRb.isKinematic = true;
        transform.SetParent(playerCam);
        transform.localPosition = new Vector3(0f, -0.2f, 1.5f);
        transform.localRotation = Quaternion.identity;
    }

    void ThrowObject()
    {
        if (!isHeld) return;
        isHeld = false;

        transform.SetParent(null);
        objectRb.isKinematic = false;
        objectRb.AddForce(playerCam.forward * 10, ForceMode.Impulse);
    }
}
