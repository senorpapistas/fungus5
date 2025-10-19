using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightMeter : MonoBehaviour
{
    public Image bg;
    public Image bar;
    public TMP_Text cooldownWarning;
    private Color defaultColor;

    private void OnEnable()
    {
        Flashlight.PowerChangeEvent += OnPowerChangeEvent;
        Flashlight.CooldownEvent += OnCooldownEvent;
    }

    private void OnDisable()
    {
        Flashlight.PowerChangeEvent -= OnPowerChangeEvent;
        Flashlight.CooldownEvent -= OnCooldownEvent;
    }

    private void Start()
    {
        defaultColor = bar.color;
        cooldownWarning.enabled = false;
    }

    void OnPowerChangeEvent(float currentPower, float maxPower)
    {
        bar.fillAmount = (float)currentPower / maxPower;
    }
    void OnCooldownEvent(bool status)
    {
        if (status)
        {
            bar.color = Color.red;
            cooldownWarning.enabled = true;
        }
        else
        {
            bar.color = defaultColor;
            cooldownWarning.enabled = false;
        }
    }
}
