using UnityEngine;

public class TerminalLogic : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource genSound;

    [SerializeField] private GameObject zoneLightsParent;
    private bool isActivated;
    public void Interact()
    {
        if (isActivated) return;

        isActivated = true;

        if(zoneLightsParent != null)
        {
            zoneLightsParent.SetActive(false);
        }

        GameEvents.FireOnTerminalActivated();

        if (genSound != null)
        {
            genSound.Stop();
        }

        Debug.Log("Générateur éteint ! La zone locale est dans le noir.");
    }
}