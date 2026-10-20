using System.Collections;
using UnityEngine;

public class LightBlinker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Light targetLight;
    [SerializeField] private MeshRenderer bulbRenderer;

    [Header("Default State (Actif Gen)")]
    [SerializeField] private Color normalColor = new Color(1f, 0.92f, 0.75f);
    [SerializeField] private float normalIntensity = 0.4f;

    [Header("Alert State (Blinking Red)")]
    [SerializeField] private Color alarmColor = Color.red;
    [SerializeField] private float alarmIntensity = 2.5f;
    [SerializeField] private float blinkInterval = 0.4f;
    [SerializeField] private bool blinkOnAwake = false;

    private Material _bulbMaterial;
    private Coroutine _blinkCoroutine;
    private static readonly int EmissionColorProp = Shader.PropertyToID("_EmissionColor");

    void Awake()
    {
        if (targetLight == null) targetLight = GetComponentInChildren<Light>();
        if (bulbRenderer == null)
        {
            Transform bulb = transform.Find("SciFi_WallLightBulb");
            if (bulb != null) bulbRenderer = bulb.GetComponent<MeshRenderer>();
        }

        if (bulbRenderer != null)
        {
            _bulbMaterial = bulbRenderer.material;
            _bulbMaterial.EnableKeyword("_EMISSION");
        }
    }

    void Start()
    {
        if (blinkOnAwake)
        {
            StartBlinking();
        }
        else
        {
            ApplyLight(normalColor, normalIntensity);
        }
    }

    public void StartBlinking()
    {
        if (_blinkCoroutine == null)
        {
            _blinkCoroutine = StartCoroutine(BlinkRoutine());
        }
    }

    public void StopBlinking()
    {
        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
            _blinkCoroutine = null;
        }
        ApplyLight(normalColor, normalIntensity);
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            ApplyLight(alarmColor, alarmIntensity);
            yield return new WaitForSeconds(blinkInterval);

            ApplyLight(Color.black, 0f);
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private void ApplyLight(Color col, float intensity)
    {
        if (targetLight != null)
        {
            targetLight.color = col;
            targetLight.intensity = intensity;
            targetLight.enabled = intensity > 0f;
        }

        if (_bulbMaterial != null)
        {
            _bulbMaterial.SetColor(EmissionColorProp, col * intensity);
        }
    }

    void OnDestroy()
    {
        if (_bulbMaterial != null)
        {
            Destroy(_bulbMaterial);
        }
    }
}
