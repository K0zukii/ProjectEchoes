using UnityEngine;

public class EscapeTrigger : MonoBehaviour
{
    [SerializeField] private VictoryManager victoryManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            victoryManager.TriggerVictory();
        }
    }
}
