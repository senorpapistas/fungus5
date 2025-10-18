using System;
using UnityEngine;

//flashlight that activates on spacebar hold

public class Flashlight : MonoBehaviour
{
    public Light flashlight;
    public GameObject hitbox;

    [Header("State")]
    [SerializeField] private bool isOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
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
    }
}
