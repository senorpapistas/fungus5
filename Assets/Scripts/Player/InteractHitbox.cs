using System;
using UnityEngine;

//Updating list of colliders with the Interactable tag
public class InteractHitbox : MonoBehaviour
{
    public Collider[] hitColliders;
    private void FixedUpdate()
    {
        CheckPickup();
    }

    void CheckPickup()
    {
        Collider firstCollider = null;
        hitColliders = Physics.OverlapBox(transform.position + transform.forward, transform.localScale, Quaternion.identity, LayerMask.GetMask("Interactable"));
        if (hitColliders.Length > 0)
        {
            firstCollider = hitColliders[0];
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
}
