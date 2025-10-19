using System;
using UnityEngine;

public class InteractItem : MonoBehaviour
{
    [SerializeField] private GameObject heldPos;
    [SerializeField] private Pickup currItem;
    Collider[] hitColliders;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currItem == null)
            {
                TryPickup();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currItem != null)
            {
                currItem.Throw();
                currItem = null;
            }
        }
    }

    private void FixedUpdate()
    {
        CheckPickup();
    }

    void TryPickup()
    {
        //Debug.Log("working?");
        //Collider[] hitColliders = Physics.OverlapBox(transform.position + transform.forward, transform.localScale, Quaternion.identity, LayerMask.GetMask("Interactable"));
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Debug.Log(hitColliders[i].gameObject.name);
            if (hitColliders[i].GetComponent<Pickup>() != null)
            {
                Debug.Log("big fart incoming");
                currItem = hitColliders[i].GetComponent<Pickup>();
                currItem.PickUp(heldPos.transform);
                break;
            }
        }
    }

    void CheckPickup()
    {
        hitColliders = Physics.OverlapBox(transform.position + transform.forward, transform.localScale, Quaternion.identity, LayerMask.GetMask("Interactable"));
        if (hitColliders.Length > 0)
        {
            Debug.Log("items nearby");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Application.isPlaying)
        {
            Gizmos.DrawWireCube(transform.position + transform.forward, transform.localScale);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Item")
        {
            Debug.Log("mmfgh");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("this worked");
                collision.transform.SetParent(heldPos.transform, false);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Item")
        {
            Debug.Log("sog assy mmmm");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("what");
                //collision.transform.SetParent(heldPos.transform, false);
            }
        }
    }
}
