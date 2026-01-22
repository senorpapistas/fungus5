using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public DungeonPlayerTracker dungeonPlayerTracker;   //may need to decouple;
    public Room room;
    public static event Action<Room> DoorEnterEvent;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (room != dungeonPlayerTracker.currentRoom)
            {
                DoorEnterEvent?.Invoke(room);
                room.CloseRoom();
            }
        }
    }
}
