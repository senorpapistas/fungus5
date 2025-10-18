using System;
using UnityEngine;

//flashlight that activates on spacebar hold

public class Flashlight : MonoBehaviour
{
    public Light flashlight;

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
            flashlight.enabled = true;
        }
        else
        {
            flashlight.enabled = false;
        }
    }
}
