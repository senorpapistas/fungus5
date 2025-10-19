using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float throwStrength = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        changePhysics(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(Transform held)
    {
        transform.SetParent(held, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        changePhysics(true);
        Physics.IgnoreLayerCollision(10, 6, true);
    }

    public void Throw()
    {
        transform.SetParent(null);
        changePhysics(false);
        Vector3 pos = transform.forward + transform.up * .75f;
        rb.AddForce(pos * throwStrength, ForceMode.Impulse);
        StartCoroutine(EnablePlayerCollision());
    }

    private void changePhysics(bool on)
    {
        if (rb != null)
        {
            rb.useGravity = !on;
            rb.isKinematic = on;
        }
    }

    private IEnumerator EnablePlayerCollision()
    {
        yield return new WaitForSeconds(1);
        Physics.IgnoreLayerCollision(10, 6, false);
    }
}
