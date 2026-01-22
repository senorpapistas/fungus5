using UnityEngine;

public class DungeonPlayerTracker : MonoBehaviour
{
    public DungeonGenerator dungeonGenerator;
    public Room currentRoom;

    private void OnEnable()
    {
        DoorTrigger.DoorEnterEvent += ChangeCurrentRoom;
        DungeonGenerator.DungeonGeneratedEvent += ResetDungeon;
    }

    private void OnDisable()
    {
        DoorTrigger.DoorEnterEvent -= ChangeCurrentRoom;
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
        ChangeCurrentRoom(dungeonGenerator.startRoom);
    }

    void ChangeCurrentRoom(Room room)
    {
        if (currentRoom != room) { Debug.Log("Player entered new room"); }
        currentRoom = room;
    }

    public void ClearCurrentRoom()
    {
        currentRoom.ClearRoom();
    }
}
