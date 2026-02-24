using TMPro;
using UnityEngine;

//shows current room data in a canvas
public class CurrentRoomData : MonoBehaviour
{
    public TMP_Text roomName;
    public TMP_Text roomCoordinates;
    public TMP_Text roomType;
    private void OnEnable()
    {
        DungeonPlayerTracker.RoomChangeEvent += UpdateText;
    }

    private void OnDisable()
    {
        DungeonPlayerTracker.RoomChangeEvent -= UpdateText;
    }


    public void UpdateText(Room room)
    {
        roomName.text = "Name: " + room.name;
        roomCoordinates.text = "Coordinates: " + room.coordinates.x + " " +room.coordinates.y;
        roomType.text = "Room Type: " + room.roomType;
    }
}
