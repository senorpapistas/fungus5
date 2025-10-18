using System;
using UnityEngine;

//flashlight that activates on spacebar hold

public class Flashlight : MonoBehaviour
{
    public Light flashlight;
    public GameObject hitbox;
    public float detectionRange = 10f;
    public LayerMask enemyLayer;

    [Header("State")]
    [SerializeField] private bool isOn;

    void Start()
    {
        if (flashlight == null)
        {
            flashlight = GetComponentInChildren<Light>();
        }
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
            TurnOn();
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
        hitbox.SetActive(true);
    }

    void TurnOff()
    {
        flashlight.enabled = false;
        hitbox.SetActive(false);

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
}
