using UnityEngine;

public class FolderItem : MonoBehaviour, IInteractable
{
    private bool _isCollected = false;

    public void Interact()
    {
        if (_isCollected) return;
        _isCollected = true;

        GameEvents.FireOnFolderCollected();

        if (SubtitleManager.Instance != null)
        {
            SubtitleManager.Instance.ShowSubtitle("I finally got the classified file... Now I just need to get the hell out of here.", 4.5f);
        }

        gameObject.SetActive(false);
    }

}
