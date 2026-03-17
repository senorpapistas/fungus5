using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public InteractHitbox interactHitbox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
            if (interactHitbox.firstCollider.GetComponent<IInteractable>() != null)
            {
                interactHitbox.firstCollider.GetComponent<IInteractable>().Use(this.gameObject);
                AudioManager.Instance.PlaySound("equip");
            }
    }
}
