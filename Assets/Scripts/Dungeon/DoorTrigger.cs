using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Room room;
    public static event Action<Room> DoorEnterEvent;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player entered");
            DoorEnterEvent?.Invoke(room);
        }
    }
}
