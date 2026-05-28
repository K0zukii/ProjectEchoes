using UnityEngine;

public class CrosshairUI : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction;

    [Header("Animation Settings")]
    [SerializeField] private float normalSize = 1f;
    [SerializeField] private float hoverSize = 2f;
    [SerializeField] private float animationSpeed = 10f;

    private Vector3 targetScale;

    void Start()
    {
        targetScale = new Vector3(normalSize, normalSize, normalSize);
    }

    void OnEnable()
    {
        playerInteraction.OnHoverStateChanged += UpdateCrosshairTarget;
    }

    void OnDisable()
    {
        playerInteraction.OnHoverStateChanged -= UpdateCrosshairTarget;
    }

    private void UpdateCrosshairTarget(bool isHovering)
    {
        float size = isHovering ? hoverSize : normalSize;
        targetScale = new Vector3(size, size, size);
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * animationSpeed);
    }
}
