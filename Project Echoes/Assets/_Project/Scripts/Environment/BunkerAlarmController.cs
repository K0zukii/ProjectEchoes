using UnityEngine;

public class BunkerAlarmController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private int triggerRemainingGenerators = 1;

    [Header("Alarm Lights")]
    [SerializeField] private LightBlinker[] alarmLights;

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
        if (remaining <= triggerRemainingGenerators)
        {
            TriggerAlarm();
        }
    }

    [ContextMenu("Triggers the Alarm")]
    public void TriggerAlarm()
    {
        if (alarmLights == null || alarmLights.Length == 0)
        {
            alarmLights = FindObjectsByType<LightBlinker>();
        }

        foreach (var light in alarmLights)
        {
            if (light != null)
            {
                light.StartBlinking();
            }
        }
    }

    [ContextMenu("Automaticaly assign every lights")]
    private void AutoAssignLights()
    {
        alarmLights = FindObjectsByType<LightBlinker>();
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
