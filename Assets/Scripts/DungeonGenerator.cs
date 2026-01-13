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
}

public class DungeonGenerator : MonoBehaviour
{
    public int size;
    public int[,] dungeon = new int[10, 10];        //make sure minroomcount IS NOT GREATER THAN size of dungeon or it will create an endless loop

    public int roomCount;

    [Header("Generation Settings")]
    public int minRoomCount;
    public int maxRoomCount;

    [Header("Prefab")]
    public GameObject room;
    private Queue<Coordinates> cellQueue = new Queue<Coordinates>();

    public List<GameObject> rooms = new List<GameObject>();

    private void Start()
    {
        SetupDungeon();
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetupDungeon();
        }
    }
    */

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
            if (roomCount > maxRoomCount) { break; }

            Coordinates temp = cellQueue.Dequeue();

            //linear dungeon
            /*
            bool created = false;
            if (!created) created = VisitCell(temp.x + 1, temp.y);
            if (!created) created = VisitCell(temp.x - 1, temp.y);
            if (!created) created = VisitCell(temp.x, temp.y + 1);
            if (!created) created = VisitCell(temp.x, temp.y - 1);
            */

            //complex dungeon
            VisitCell(temp.x + 1, temp.y);
            VisitCell(temp.x - 1, temp.y);
            VisitCell(temp.x, temp.y + 1);
            VisitCell(temp.x, temp.y - 1);
        }

        Debug.Log("finished generating!");

        if (roomCount < minRoomCount) { minRoomCount--;  SetupDungeon(); }      //minroomcount-- to prevent endlessly generating a dungeon that can't be made
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

        Coordinates bruh = new Coordinates();
        bruh.x = x; bruh.y = y;
        cellQueue.Enqueue(bruh);

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
        newRoom.name = $"{x}" + $",{y}";
        rooms.Add(newRoom);
    }
}
