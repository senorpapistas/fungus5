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
    public int size;
    public int[,] dungeon = new int[10, 10];        //make sure minroomcount IS NOT GREATER THAN size of dungeon or it will create an endless loop

    public int roomCount;

    [Header("Generation Settings")]
    public int minRoomCount;
    private int tempMinRoomCount;
    public int maxRoomCount;

    [Header("Prefab")]
    public GameObject room;

    private Queue<Coordinates> cellQueue = new Queue<Coordinates>();
    public List<GameObject> rooms = new List<GameObject>();
    public List<Coordinates> endRooms = new List<Coordinates>();

    public List<GameObject> endRoomGameobjects = new List<GameObject>();

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

        foreach (GameObject temp in rooms)
        {
            Destroy(temp);
        }
        rooms.Clear();
        endRooms.Clear();
        endRoomGameobjects.Clear();
        roomCount = 0;

        cellQueue.Clear();

        VisitCell(4, 4);

        Debug.Log("finished setting up!");

        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        while (cellQueue.Count > 0)
        {
            //if (roomCount > maxRoomCount) { break; }

            Coordinates temp = cellQueue.Dequeue();

            bool created = false;

            //linear dungeon
            /*
            if (!created) created = VisitCell(temp.x + 1, temp.y);
            if (!created) created = VisitCell(temp.x - 1, temp.y);
            if (!created) created = VisitCell(temp.x, temp.y + 1);
            if (!created) created = VisitCell(temp.x, temp.y - 1);
            */

            //complex dungeon
            created |= VisitCell(temp.x + 1, temp.y);
            created |= VisitCell(temp.x - 1, temp.y);
            created |= VisitCell(temp.x, temp.y + 1);
            created |= VisitCell(temp.x, temp.y - 1);

            Debug.Log($"queue is processing {temp.x},{temp.y}, created is {created}");

            if (created == false) { Debug.Log("endroom is " + temp.x + " " + temp.y); endRooms.Add(new Coordinates(temp.x, temp.y)); }
        }

        if (roomCount < minRoomCount) { Debug.Log("generation failed. restarting with 1 less room..."); tempMinRoomCount--;  SetupDungeon(); return; }      //tempminroomcount-- to prevent endlessly generating a dungeon that can't be made

        GenerateEndRooms();

        Debug.Log($"Finished Generating with room count of {roomCount}!");
        tempMinRoomCount = minRoomCount;
    }

    
    void GenerateEndRooms()
    {
        foreach (var room in endRooms)
        {
            string name = $"{room.x}" + $",{room.y}";
            Debug.Log("generateendrooms looking for " + name);

            GameObject endRoom = null;
            foreach(var temp in rooms)
            {
                if (temp.name == name) { endRoom = temp; }
            }

            if (endRoom != null)
            {
                Debug.Log(name + " has been found");
                endRoomGameobjects.Add(endRoom);
                endRoom.name += " ENDROOM";
                endRoom.GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }
    

    bool VisitCell(int x, int y)
    {
        if (x < 0 || y < 0) { return false; }
        if (x >= dungeon.GetLength(0) || y >= dungeon.GetLength(1) || UnityEngine.Random.value <.50f) { return false; }

        if (dungeon[x, y] == 1 || CheckNeighbors(x,y) > 1)
        {
            return false;
        }

        roomCount++;
        dungeon[x, y] = 1;
        SpawnRoom(x, y);

        Coordinates temp = new Coordinates(x,y);
        cellQueue.Enqueue(temp);
        //Debug.Log($"put {x},{y} in queue");

        return true;
    }

    int CheckNeighbors(int x, int y)
    {
        int result = 0;
        Debug.Log("checking neighbors of " + $"{x}" + $",{y}");
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

    void SpawnRoom(int x, int y)
    {
        Vector3 position = new Vector3(x * size, 0, y * size) + transform.position;
        GameObject newRoom = Instantiate(room, position, quaternion.identity);
        newRoom.SetActive(true);
        newRoom.name = $"{x}" + $",{y}";
        rooms.Add(newRoom);
    }
}
