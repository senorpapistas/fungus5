using UnityEngine;
using UnityEngine.Timeline;

public class InteractableMarker : MonoBehaviour
{
    public GameObject marker;
    public float offset;

    private Collider firstCollider;

    private void OnEnable()
    {
        InteractHitbox.FirstInteractableInsideColliderEvent += CheckFirstCollider;
    }

    private void OnDisable()
    {

        InteractHitbox.FirstInteractableInsideColliderEvent -= CheckFirstCollider;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (firstCollider != null)
        {
            ShowMarker(firstCollider.gameObject);
        }
        else
        {
            HideMarker();
        }
    }

    void CheckFirstCollider(Collider _firstCollider)
    {
        firstCollider = _firstCollider;
    }

    void ShowMarker(GameObject Interactable)
    {
        marker.SetActive(true);
        marker.transform.position = new Vector3(Interactable.transform.position.x, Interactable.transform.position.y + Interactable.GetComponent<Collider>().bounds.size.y + offset, Interactable.transform.position.z);
    }

    void HideMarker()
    {
        marker.SetActive(false);
    }
}
