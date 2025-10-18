using UnityEngine;
using UnityEngine.UI;

public class FlashlightMeter : MonoBehaviour
{
    public Image bg;
    public Image bar;

    private void OnEnable()
    {
        Flashlight.PowerChangeEvent += OnPowerChangeEvent;
    }

    private void OnDisable()
    {
        Flashlight.PowerChangeEvent -= OnPowerChangeEvent;
    }

    private void Start()
    {
    }

    void OnPowerChangeEvent(float currentPower, float maxPower)
    {
        bar.fillAmount = (float)currentPower / maxPower;
    }
}
