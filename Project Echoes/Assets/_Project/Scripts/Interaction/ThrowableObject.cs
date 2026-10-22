using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

[RequireComponent(typeof(NoiseEmitter))]
public class ThrowableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform playerCam;
    [SerializeField] private PlayerInputHandler playerInput;
    [SerializeField] private float throwForce = 10f;

    [Header("Collision Settings")]
    [SerializeField] private float minBounceVelocity = 1.5f;
    [SerializeField] private float bounceCooldown = 0.2f;

    private Rigidbody objectRb;
    private NoiseEmitter noiseEmitter;
    private bool isHeld = false;
    private float lastBounceTime;

    void Awake()
    {
        objectRb = GetComponent<Rigidbody>();
        noiseEmitter = GetComponent<NoiseEmitter>();
    }

    void OnEnable()
    {
        if (playerInput != null)
            playerInput.IsThrowing += ThrowObject;
    }

    void OnDisable()
    {
        if (playerInput != null)
            playerInput.IsThrowing -= ThrowObject;
    }
    public void Interact()
    {
        if (isHeld) return;
        isHeld = true;

        objectRb.isKinematic = true;
        transform.SetParent(playerCam);
        transform.SetLocalPositionAndRotation(new Vector3(0f, -0.2f, 0.8f), Quaternion.identity);
    }

    void ThrowObject()
    {
        if (!isHeld) return;
        isHeld = false;

        transform.SetParent(null);
        objectRb.isKinematic = false;
        objectRb.AddForce(playerCam.forward * throwForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastBounceTime < bounceCooldown) return;
        if (collision.relativeVelocity.magnitude < minBounceVelocity) return;

        lastBounceTime = Time.time;
        noiseEmitter.EmitNoise(20f);
        noiseEmitter.PlaySound(true, 1);
    }
}
