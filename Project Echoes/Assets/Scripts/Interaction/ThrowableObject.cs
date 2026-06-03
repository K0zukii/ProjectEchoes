using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrowableObject : MonoBehaviour, IInteractable
{
    private Rigidbody objectRb;
    [SerializeField] private Transform playerCam;
    [SerializeField] private PlayerInputHandler playerInput;
    private NoiseEmitter noiseEmitter;
    private bool isHeld = false;

    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        noiseEmitter = GetComponent<NoiseEmitter>();

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
        transform.SetLocalPositionAndRotation(new Vector3(0f, -0.2f, 1.5f), Quaternion.identity);
    }

    void ThrowObject()
    {
        if (!isHeld) return;
        isHeld = false;

        transform.SetParent(null);
        objectRb.isKinematic = false;
        objectRb.AddForce(playerCam.forward * 10, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        noiseEmitter.EmitNoise(10f);
        noiseEmitter.PlaySound(true, 1);
    }
}
