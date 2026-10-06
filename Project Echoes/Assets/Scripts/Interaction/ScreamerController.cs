using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Camera))]

[RequireComponent(typeof(AudioSource))]
public class ScreamerController : MonoBehaviour
{
    [Header("Camera & Target")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float monsterHeadHeight = 1.6f;

    [Header("Visuals Settings")]
    [SerializeField] private float duration = 1.2f;
    [SerializeField] private float snapSpeed = 25f;
    [SerializeField] private float zoomFov = 45f;

    [Header("Camera Shake")]
    [SerializeField] private float shakeIntensity = 0.12f;
    [SerializeField] private float rollShake = 4f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip screamerSound;

    [Header("Light")]
    [SerializeField] private Light screamerLight;

    private Vector3 initialLocalPos;
    void Awake()
    {
        if (playerCamera == null) playerCamera = GetComponent<Camera>();

        if (playerCamera != null)
        {
            initialLocalPos = playerCamera.transform.localPosition;
        }

        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        GameEvents.OnPlayerCaughtByMonster += LaunchScreamer;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerCaughtByMonster -= LaunchScreamer;
    }

    private void LaunchScreamer(Transform monsterTransform)
    {
        StartCoroutine(ScreamerRoutine(monsterTransform));
    }

    private IEnumerator ScreamerRoutine(Transform monsterTransform)
    {
        if (screamerLight != null)
        {
            screamerLight.enabled = true;
        }

        if (audioSource != null && screamerSound != null)
        {
            audioSource.PlayOneShot(screamerSound);
        }

        Transform camTrans = playerCamera.transform;
        Vector3 targetPos = monsterTransform.position + Vector3.up * monsterHeadHeight;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            Vector3 direction = (targetPos - camTrans.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                camTrans.rotation = Quaternion.Slerp(camTrans.rotation, targetRot, Time.unscaledDeltaTime * snapSpeed);
            }

            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFov, Time.unscaledDeltaTime * 15);

            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
            camTrans.localPosition = initialLocalPos + shakeOffset;

            float zTilt = (Random.value - 0.5f) * rollShake;
            camTrans.rotation *= Quaternion.Euler(0f, 0f, zTilt);

            yield return null;
        }
    }
}
