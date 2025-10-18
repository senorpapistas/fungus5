using System;
using UnityEngine;

//flashlight that activates on spacebar hold

public class Flashlight : MonoBehaviour
{
    public Light flashlight;
    public LayerMask enemyLayer;
    public static event Action<float, float> PowerChangeEvent;

    [Header("Variables")]
    public float detectionRange = 10f;
    public float maxPower;
    public float powerUseRate;
    public float powerRechargeRate;


    [Header("State")]
    [SerializeField] private bool isOn;
    [SerializeField] public float currentPower;

    void Start()
    {
        if (flashlight == null)
        {
            flashlight = GetComponentInChildren<Light>();
        }

        currentPower = maxPower;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isOn = true;
        }
        else
        {
            isOn = false;
        }

        if (isOn)
        {
            ConsumePower();
            if (currentPower > 0)
            {
                TurnOn();
            }
            else
            {
                TurnOff();
            }
        }
        else
        {
            TurnOff();
        }

        if (flashlight.enabled)
        {
            DetectEnemiesInBeam();
        }
    }

    void TurnOn()
    {
        flashlight.enabled = true;
    }

    void TurnOff()
    {
        flashlight.enabled = false;
        RechargePower();

        // Call OnFlashlightExit() for enemies within range
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, detectionRange, enemyLayer);
        foreach (Collider enemyCollider in enemiesInRange)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.OnFlashlightExit();
            }
        }
    }

    public bool IsObjectInBeam(Vector3 objectPosition)
    {
        // If flashlight turned off
        if (!flashlight.enabled) return false;

        // Initial check to see if flashlight can see location
        Vector3 directionToObject = objectPosition - transform.position;
        float distanceToObject = directionToObject.magnitude;

        if (distanceToObject > detectionRange)
        {
            return false;
        }

        // Angle check
        float angle = Vector3.Angle(transform.forward, directionToObject);
        return angle <= flashlight.spotAngle * 0.5f;
    }

    private void DetectEnemiesInBeam()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, detectionRange, enemyLayer);

        foreach (Collider enemyCollider in enemiesInRange)
        {
            Vector3 enemyPosition = enemyCollider.transform.position;
            Debug.Log("See enemy in beam");

            if (IsObjectInBeam(enemyPosition))
            {
                Debug.Log("See enemy in direct beam range");
                Enemy enemy = enemyCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.OnFlashlightHit();
                }
            }
            else
            {
                Enemy enemy = enemyCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.OnFlashlightExit();
                }
            }
        }
    }

    public void SetIntensity(float intensity)
    {
        flashlight.intensity = intensity;
    }

    public void Toggle()
    {
        flashlight.enabled = !flashlight.enabled;
    }

    private void ChangePower(float change)
    {
        currentPower += change;
        PowerChangeEvent?.Invoke(currentPower,maxPower);
    }

    public void ConsumePower()
    {
        ChangePower(-powerUseRate * Time.deltaTime);
        if (currentPower < 0) { currentPower = 0; }
    }

    public void RechargePower()
    {
        if (currentPower < maxPower) { ChangePower(powerRechargeRate * Time.deltaTime); }
        if (currentPower > maxPower) { currentPower = maxPower; }
    }
}
