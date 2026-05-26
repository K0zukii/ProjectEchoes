using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private PlayerStamina playerStamina;

    void OnEnable()
    {
        playerStamina.OnStaminaChanged += UpdateBar;
    }

    void OnDisable()
    {
        playerStamina.OnStaminaChanged -= UpdateBar;
    }

    void UpdateBar(float ratio)
    {
        bar.fillAmount = ratio;

        if (bar.fillAmount != 1)
        {
            Color barColor = bar.color;
            barColor.a = 255;
            bar.color = barColor;
        }
        else
        {
            Color barColor = bar.color;
            barColor.a = 0;
            bar.color = barColor;
        }
    }
}
