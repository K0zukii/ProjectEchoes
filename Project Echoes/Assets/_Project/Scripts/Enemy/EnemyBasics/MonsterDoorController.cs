using UnityEngine;

public class MonsterDoorController : MonoBehaviour
{
    [Header("Elements To Remove")]
    [SerializeField] private GameObject barsToDisable;

    [Header("Optional")]
    [SerializeField] private AudioSource doorAudioSource;
    [SerializeField] private AudioClip metalBreakClip;

    void OnEnable()
    {
        GameEvents.OnGeneratorDeactivated += HandleGeneratorDeactivated;
    }

    void OnDisable()
    {
        GameEvents.OnGeneratorDeactivated -= HandleGeneratorDeactivated;
    }

    private void HandleGeneratorDeactivated(int remaining)
    {
        if (remaining == 1)
        {
            OpenMonsterCell();
        }
    }

    private void OpenMonsterCell()
    {
        if (barsToDisable != null)
        {
            barsToDisable.SetActive(false);
        }

        if (doorAudioSource != null && metalBreakClip != null)
        {
            doorAudioSource.PlayOneShot(metalBreakClip);
        }
    }
}
