using System;
using UnityEngine;

//Updating list of colliders with the Interactable tag
public class InteractHitbox : MonoBehaviour
{
    [SerializeField] private Collider[] hitColliders;
    public Collider firstCollider;
    public static event Action<Collider> FirstInteractableInsideColliderEvent;
    private void FixedUpdate()
    {
        CheckPickup();
    }

    void CheckPickup()
    {
        hitColliders = Physics.OverlapBox(transform.position + transform.forward, transform.localScale, Quaternion.identity, LayerMask.GetMask("Interactable"));

        if (hitColliders.Length > 0 )
        {
            firstCollider = hitColliders[0];
        }
        else
        {
            firstCollider = null;
        }
        FirstInteractableInsideColliderEvent?.Invoke(firstCollider);
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
