using UnityEngine;
using System.Collections;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI subtitleText;
    private Coroutine _currentSubtitleCoroutine;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (subtitleText != null)
        {
            subtitleText.text = "";
        }
    }

    public void ShowSubtitle(string text, float duration = 4f)
    {
        if (_currentSubtitleCoroutine != null)
        {
            StopCoroutine(_currentSubtitleCoroutine);
        }
        _currentSubtitleCoroutine = StartCoroutine(DisplaySubtitleRoutine(text, duration));
    }

    private IEnumerator DisplaySubtitleRoutine(string text, float duration)
    {
        subtitleText.text = text;
        yield return new WaitForSeconds(duration);
        subtitleText.text = "";
        _currentSubtitleCoroutine = null;
    }
}
