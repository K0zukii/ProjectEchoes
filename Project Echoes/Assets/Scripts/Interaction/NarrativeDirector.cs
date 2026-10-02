using UnityEngine;
using System.Collections;

public class NarrativeDirector : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    void OnEnable()
    {
        GameEvents.OnTerminalActivated += HandleGenerators;
    }

    void OnDisable()
    {
        GameEvents.OnTerminalActivated -= HandleGenerators;
    }

    private IEnumerator PlayIntroSequence()
    {
        yield return new WaitForSeconds(1.5f);
        SubtitleManager.Instance.ShowSubtitle("All this for a damn corporate folder... How did my life end up like this?", 4f);

        yield return new WaitForSeconds(4.5f);
        SubtitleManager.Instance.ShowSubtitle("The blast door sealed shut behind me. Without the passcode, I have to cut the facility's power to force a manual override.", 5f);
    }

    private void HandleGenerators()
    {
        int remaining = GameManager.Instance.GeneratorsRemaining;

        if (remaining == 2)
        {
            SubtitleManager.Instance.ShowSubtitle("One terminal down. Two left before the backup power cuts completely.", 3.5f);
        }
        else if (remaining == 1)
        {
            StartCoroutine(MonsterBreachReaction());
        }
        else if (remaining == 0)
        {
            SubtitleManager.Instance.ShowSubtitle("The facility is completely dark. The main airlock should be unlocked now!", 4f);
        }
    }

    private IEnumerator MonsterBreachReaction()
    {
        yield return new WaitForSeconds(1f);
        SubtitleManager.Instance.ShowSubtitle("What the hell was that scream...?! Something else is locked down here with me.", 4.5f);
    }
}
