using UnityEngine;

public class BombNotification : MonoBehaviour
{
    public GameObject notification;

    private void OnEnable()
    {
        InteractItem.ItemHeldEvent += OnItemHeldEvent;
    }

    private void OnItemHeldEvent(bool itemHeld)
    {
        notification.SetActive(itemHeld);
    }

    private void Start()
    {
        notification.SetActive(false);
    }
}
