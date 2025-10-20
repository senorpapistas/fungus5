using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private float value;

    public void SetValue(float amount)
    {
        value = amount;
    }

    public float GetValue()
    {
        return value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWallet wallet = other.GetComponent<PlayerWallet>();
            if (wallet != null)
            {
                Debug.Log($"Player has collected money! +{value}");
                wallet.AddMoney(value);
                Destroy(gameObject);
            }
        }
    }
}