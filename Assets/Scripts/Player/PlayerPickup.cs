using System;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public InteractHitbox interactHitbox;
    [SerializeField] public GameObject heldPos;
    [SerializeField] private Pickup currPickup;

    public static event Action<bool> PickupHeldEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currPickup != null)
            {
                currPickup.Throw();
                currPickup = null;

                AudioManager.Instance.PlaySound("whoosh");
                PickupHeldEvent?.Invoke(false);
            }
        }
    }
}
