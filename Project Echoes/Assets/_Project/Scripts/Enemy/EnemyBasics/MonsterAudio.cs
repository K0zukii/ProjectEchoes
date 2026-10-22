using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MonsterAudio : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource roarSource;
    [SerializeField] private AudioSource footstepSource;

    [Header("Clips")]
    [SerializeField] private AudioClip releaseRoarClip;
    [SerializeField] private AudioClip[] footstepClips;

    [Header("Pitch Variation")]
    [SerializeField] private float minPitch = 0.85f;
    [SerializeField] private float maxPitch = 1.15f;

    void OnEnable()
    {
        GameEvents.OnGeneratorDeactivated += TriggerReleaseRoar;
    }

    void OnDisable()
    {
        GameEvents.OnGeneratorDeactivated -= TriggerReleaseRoar;
    }

    private void TriggerReleaseRoar(int remaining)
    {
        if (remaining == 1 && releaseRoarClip != null && roarSource != null)
        {
            roarSource.PlayOneShot(releaseRoarClip);
        }
    }

    public void PlayFootstep()
    {
        if (footstepClips == null || footstepClips.Length == 0 || footstepSource == null) return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];

        footstepSource.pitch = Random.Range(minPitch, maxPitch);
        footstepSource.PlayOneShot(clip);
    }
}
