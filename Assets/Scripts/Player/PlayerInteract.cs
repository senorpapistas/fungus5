using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public InteractHitbox interact;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        for (int i = 0; i < interact.hitColliders.Length; i++)
        {
            if (interact.hitColliders[i].GetComponent<IInteractable>() != null)
            {
                interact.hitColliders[i].GetComponent<IInteractable>().Use();
                AudioManager.Instance.PlaySound("equip");
                break;
            }
        }
    }
}
