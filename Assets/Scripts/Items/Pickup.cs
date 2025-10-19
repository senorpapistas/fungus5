using System.Collections;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _throwStrength = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        changePhysics(false);
    }

    public virtual void PickUp(Transform held)
    {
        transform.SetParent(held, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        changePhysics(true);
        GetComponent<Collider>().enabled = false;
        //Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), true);
    }

    public virtual void Throw()
    {
        transform.SetParent(null);
        changePhysics(false);
        Vector3 pos = transform.forward + transform.up * .75f;
        _rb.AddForce(pos * _throwStrength, ForceMode.Impulse);
        StartCoroutine(EnablePlayerCollision());
    }

    public void changePhysics(bool on)
    {
        if (_rb != null)
        {
            _rb.useGravity = !on;
            _rb.isKinematic = on;
        }
    }

    protected IEnumerator EnablePlayerCollision()
    {
        yield return new WaitForSeconds(.3f);
        GetComponent<Collider>().enabled = true;
        //Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);
    }
}
