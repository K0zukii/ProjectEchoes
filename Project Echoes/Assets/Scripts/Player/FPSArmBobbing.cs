using UnityEngine;

public class FPSArmBobbing : MonoBehaviour
{
    [System.Serializable]
    public struct BobbingProfile
    {
        public float speed;
        public float amount;
    }

    [Header("References")]
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Movement Settings")]
    public BobbingProfile idleProfile = new BobbingProfile { speed = 2f, amount = 0.006f };
    public BobbingProfile walkProfile = new BobbingProfile { speed = 9f, amount = 0.04f };
    public BobbingProfile runProfile = new BobbingProfile { speed = 11f, amount = 0.06f };
    public BobbingProfile crouchProfile = new BobbingProfile { speed = 7f, amount = 0.015f };

    [Header("Run Threshold")]
    public float runSpeedThreshold = 4.5f;

    [Header("Smoothing")]
    public float smoothSpeed = 10f;

    private float timer = 0f;
    private bool isPlayingActionAnimation = false;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;

        if (inputHandler == null || playerMovement == null)
        {
            Debug.LogError($"[{gameObject.name}] FPSArmBobbing requiert PlayerInputHandler et PlayerMovement sur ses parents !");
            return;
        }

        inputHandler.IsInteracting += () => SetActionPlaying(true);
    }

    void Update()
    {
        if (inputHandler == null || playerMovement == null) return;

        bool isTryingToMove = inputHandler.MoveInput != Vector2.zero;
        float currentSpeed = isTryingToMove ? playerMovement.currentSpeed : 0f;
        
        Vector3 targetPosition = originalPosition;

        if (isPlayingActionAnimation)
        {
            
        }
        else if (inputHandler.IsCrouching)
        {
            BobbingProfile activeProfile = crouchProfile;
            if (currentSpeed > 0.1f)
            {
                timer += Time.deltaTime * activeProfile.speed;
                targetPosition.y += Mathf.Sin(timer) * activeProfile.amount;
                targetPosition.x += Mathf.Cos(timer * 0.5f) * activeProfile.amount * 0.4f;
            }
            else
            {
                timer += Time.deltaTime * idleProfile.speed * 0.7f;
                targetPosition.y += Mathf.Sin(timer) * (idleProfile.amount * 0.6f);
            }
        }
        else if (currentSpeed < 0.1f)
        {
            BobbingProfile activeProfile = idleProfile;
            timer += Time.deltaTime * activeProfile.speed;
            targetPosition.y += Mathf.Sin(timer) * activeProfile.amount;
        }
        else if (currentSpeed > runSpeedThreshold)
        {
            BobbingProfile activeProfile = runProfile;
            timer += Time.deltaTime * activeProfile.speed;
            targetPosition.y += Mathf.Sin(timer) * activeProfile.amount;
            targetPosition.x += Mathf.Cos(timer * 0.5f) * activeProfile.amount * 0.8f;
        }
        else
        {
            BobbingProfile activeProfile = walkProfile;
            timer += Time.deltaTime * activeProfile.speed;
            targetPosition.y += Mathf.Sin(timer) * activeProfile.amount;
            targetPosition.x += Mathf.Cos(timer * 0.5f) * activeProfile.amount * 0.6f;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothSpeed);
    }

    public void SetActionPlaying(bool playing)
    {
        isPlayingActionAnimation = playing;
    }
}