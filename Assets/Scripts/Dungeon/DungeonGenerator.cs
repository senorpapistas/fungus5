using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.Loading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;

[System.Serializable]
public class Coordinates
{
    public int x;
    public int y;

    public Coordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class DungeonGenerator : MonoBehaviour
{
    public int[,] dungeon = new int[10, 10];        //make sure minroomcount IS NOT GREATER THAN size of dungeon or it will create an endless loop

    public int roomCount;

    [Header("Generation Settings")]
    public int roomGap;     //gap between rooms
    public int minRoomCount;
    private int tempMinRoomCount;
    public int maxRoomCount;

    [Header("Prefab")]
    public Room roomPrefab;

    private Queue<Room> cellQueue = new Queue<Room>();

    [Header("Generated Room Results")]
    public List<Room> rooms = new List<Room>();
    public List<Room> endRooms = new List<Room>();

    public Room startRoom;
    public Room exitRoom;
    public Room shopRoom;

    [Header("Other")]
    public GameObject player;
    public static event Action DungeonGeneratedEvent;

    private void Start()
    {
        tempMinRoomCount = minRoomCount;

        SetupDungeon();
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetupDungeon();
        }
    }
    

    void SetupDungeon()
    {
        for (int i = 0; i < dungeon.GetLength(0); i++)
        {
            for (int j = 0; j < dungeon.GetLength(1); j++)
            {
                dungeon[i, j] = 0;
            }
        }

        foreach (var temp in rooms)
        {
            Destroy(temp.gameObject);
        }

        roomCount = 0;
        cellQueue.Clear();
        rooms.Clear();
        endRooms.Clear();

        if (exitRoom != null) {Destroy(exitRoom); exitRoom = null; }
        if (shopRoom != null) { Destroy(shopRoom); shopRoom = null; }
        if (startRoom != null) {shopRoom = null; }

        VisitCell(4, 4);

        Debug.Log("finished setting up!");

        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        while (cellQueue.Count > 0)
        {
            Room temp = cellQueue.Dequeue();

            bool created = false;

            //linear dungeon
            /*
            if (!created) created = VisitCell(temp.x + 1, temp.y);
            if (!created) created = VisitCell(temp.x - 1, temp.y);
            if (!created) created = VisitCell(temp.x, temp.y + 1);
            if (!created) created = VisitCell(temp.x, temp.y - 1);
            */

            //complex dungeon
            created |= VisitCell(temp.coordinates.x + 1, temp.coordinates.y);
            created |= VisitCell(temp.coordinates.x - 1, temp.coordinates.y);
            created |= VisitCell(temp.coordinates.x, temp.coordinates.y + 1);
            created |= VisitCell(temp.coordinates.x, temp.coordinates.y - 1);

            //Debug.Log($"queue is processing {temp.x},{temp.y}, created is {created}");

            if (created == false) 
            {
                //Debug.Log("endroom is " + temp.x + " " + temp.y);
                endRooms.Add(temp);
                
            }
        }

        if (roomCount < minRoomCount) { Debug.Log("generation failed. restarting with 1 less room..."); tempMinRoomCount--;  SetupDungeon(); return; }      //tempminroomcount-- to prevent endlessly generating a dungeon that can't be made
        if (endRooms.Count < 0) { Debug.Log("couldn't generate enough end rooms. restarting with 1 more room..."); tempMinRoomCount++; SetupDungeon(); return; }    //untested

        GenerateEndRooms();
        GenerateExitRoom();
        GenerateShopRoom();
        GenerateDoors();

        Debug.Log($"Finished Generating with room count of {roomCount}!");
        tempMinRoomCount = minRoomCount;

        startRoom = rooms[0];
        PlacePlayer();
        DungeonGeneratedEvent.Invoke();
    }
        
    void GenerateEndRooms()
    {
        foreach (Room room in endRooms)
        {
            room.SetupRoom(room.coordinates, RoomType.End, this);
        }
    }

    void GenerateExitRoom()
    {
        int rand = UnityEngine.Random.Range(0, endRooms.Count);

        exitRoom = endRooms[rand];

        endRooms.Remove(exitRoom);

        exitRoom.SetupRoom(exitRoom.coordinates, RoomType.Exit, this);
    }

    void GenerateShopRoom()
    {
        int rand = UnityEngine.Random.Range(0, endRooms.Count);

        shopRoom = endRooms[rand];

        endRooms.Remove(shopRoom);

        shopRoom.SetupRoom(shopRoom.coordinates, RoomType.Shop, this);
    }

    void GenerateDoors()
    {
        foreach (Room room in rooms)
        {
            room.SetupDoors(dungeon);
        }
    }

    void PlacePlayer()
    {
        if (player != null)
        {
            player.transform.position = startRoom.transform.position + new Vector3(0, 1f, 0);
        }
    }

    bool VisitCell(int x, int y)
    {
        if (roomCount >= maxRoomCount) { return false; }
        if (x < 0 || y < 0) { return false; }
        if (x >= dungeon.GetLength(0) || y >= dungeon.GetLength(1) || UnityEngine.Random.value <.50f) { return false; }

        if (dungeon[x, y] == 1 || CheckNeighbors(x,y) > 1)
        {
            return false;
        }

        roomCount++;
        dungeon[x, y] = 1;
        Room temp = SpawnRoom(x, y);

        cellQueue.Enqueue(temp);
        //Debug.Log($"put {x},{y} in queue");

        return true;
    }

    int CheckNeighbors(int x, int y)
    {
        int result = 0;
        //bug.Log("checking neighbors of " + $"{x}" + $",{y}");
        //x
        if (x == 0)
        {
            result += dungeon[x + 1, y];

        }
        else if (x == dungeon.GetLength(0)-1)
        {
            result += dungeon[x - 1, y];

        }
        else
        {
            result += dungeon[x + 1, y] + dungeon[x - 1, y];
        }

        //y
        if (y == 0)
        {
            result += dungeon[x, y + 1];
        }
        else if (y == dungeon.GetLength(1) - 1)
        {
            result += dungeon[x, y - 1];
        }
        else
        {
            result += dungeon[x, y + 1] + dungeon[x, y - 1];
        }

        return result;
    }

    Room SpawnRoom(int x, int y)
    {
        Vector3 position = new Vector3(x * (roomPrefab.planeMesh.bounds.size.x + roomGap), 0, y * (roomPrefab.planeMesh.bounds.size.z + roomGap)) + transform.position;
        Room newRoom = Instantiate(roomPrefab, position, quaternion.identity);
        newRoom.SetupRoom(new Coordinates(x, y), RoomType.Normal, this);
        rooms.Add(newRoom);
        return newRoom;
    }
}
