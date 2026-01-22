using UnityEngine;

public class DungeonPlayerTracker : MonoBehaviour
{
    public DungeonGenerator dungeonGenerator;
    public Room currentRoom;

    private void OnEnable()
    {
        DoorTrigger.DoorEnterEvent += ChangePlayerLocation;
        DungeonGenerator.DungeonGeneratedEvent += ResetDungeon;
    }

    private void OnDisable()
    {
        DoorTrigger.DoorEnterEvent -= ChangePlayerLocation;
        DungeonGenerator.DungeonGeneratedEvent -= ResetDungeon;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetDungeon()
    {
        ChangePlayerLocation(dungeonGenerator.startRoom);
    }

    void ChangePlayerLocation(Room room)
    {
        currentRoom = room;
    }

    public void OpenRoom()
    {
        currentRoom.OpenDoors();
    }
}
