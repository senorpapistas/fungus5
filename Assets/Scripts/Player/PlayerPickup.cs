using System;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public InteractHitbox interact;
    [SerializeField] private GameObject heldPos;
    [SerializeField] private Pickup currPickup;

    public static event Action<bool> PickupHeldEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currPickup == null)
            {
                TryPickup();
            }
            else if (currPickup != null)
            {
                currPickup.Throw();
                currPickup = null;

                AudioManager.Instance.PlaySound("whoosh");
                PickupHeldEvent?.Invoke(false);
            }
        }
    }

    void TryPickup()
    {
        for (int i = 0; i < interact.hitColliders.Length; i++)
        {
            if (interact.hitColliders[i].GetComponent<Pickup>() != null)
            {
                currPickup = interact.hitColliders[i].GetComponent<Pickup>();
                currPickup.PickUp(heldPos.transform);
                AudioManager.Instance.PlaySound("equip");
                PickupHeldEvent?.Invoke(true);
                break;
            }
        }
    }

}
