using UnityEngine;

public class NoiseEmitter : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private AudioSource collisionAudio;
    public void EmitNoise(float radius)
    {
        Collider[] overlapResults = Physics.OverlapSphere(transform.position, radius, enemyLayer);
        for (int i = 0; i < overlapResults.Length; i++)
        {
            if (overlapResults.Length > 0 && !Physics.Raycast(transform.position + Vector3.up * 0.5f, (overlapResults[i].transform.position - transform.position).normalized, Vector3.Distance(transform.position, overlapResults[i].transform.position), obstacleLayer))
            {
                GameEvents.FireOnNoiseEmiteed(transform.position, transform, radius);
            }
        }
    }

    void OnEnable()
    {
        GameEvents.OnPlayerCaught += StopAudio;
        GameEvents.OnGameWon += StopAudio;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerCaught -= StopAudio;
        GameEvents.OnGameWon -= StopAudio;
    }

    private void StopAudio()
    {
        if (collisionAudio != null)
        {
            collisionAudio.Stop();
        }
        enabled = false;
    }

    public void PlaySound(bool state, float pitch)
    {
        if (!enabled) return;

        collisionAudio.pitch = pitch;
        if (state == true && !collisionAudio.isPlaying)
        {
            collisionAudio.Stop();
            collisionAudio.Play();
        }
        else if (state == false)
        {
            collisionAudio.Stop();
        }
    }
}
