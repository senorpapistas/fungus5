using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject enemy_explosion;
    private bool once = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        once = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 7, LayerMask.GetMask("Enemy"));
        foreach (Collider enemy in colliders)
        {
            Rigidbody rb = enemy.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(1000, transform.position, 7);
                GameObject eprtcl = Instantiate(enemy_explosion, rb.transform.position, Quaternion.identity);
                Destroy(eprtcl, 2f);
                rb.useGravity = true;
            }
            if (!once)
            {
                GameObject prtcl = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(prtcl, 2f);
                once = true;
                Destroy(this.gameObject);
            }
        }
    }
}
