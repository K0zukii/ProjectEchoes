using UnityEngine;

public class BatteryUI : MonoBehaviour
{
    [SerializeField] private FlashLightController flashLight;
    [SerializeField] private GameObject[] batteryBlocks;

    void OnEnable()
    {
        flashLight.OnBatteryChanged += UpdateBatteryUI;
    }

    void OnDisable()
    {
        flashLight.OnBatteryChanged -= UpdateBatteryUI;
    }

    private void UpdateBatteryUI(float currentBattery)
    {
        if (batteryBlocks.Length < 4) return;

        batteryBlocks[0].SetActive(currentBattery > 0f);
        batteryBlocks[1].SetActive(currentBattery > 25f);
        batteryBlocks[2].SetActive(currentBattery > 50f);
        batteryBlocks[3].SetActive(currentBattery > 75f);
    }
}
