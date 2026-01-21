using System.Collections.Generic;
using UnityEngine;

public enum RoomType {Normal,End,Exit,Shop}

[System.Serializable]
public class Room : MonoBehaviour
{
    private DungeonGenerator dungeonGenerator;

    [Header("Room Data")]
    public Coordinates coordinates;
    public RoomType roomType;

    [Space(10)]
    [Header("Mesh")]
    public MeshRenderer planeMesh;      //the mesh of the floor (to find the room's size to be passedd on to DungeonGenerator)
    Renderer[] childArray;

    [Header("Walls")]
    public List<GameObject> walls = new List<GameObject>();

    [Header("Doors")]
    public List<Door> doors = new List<Door>();
    public List<Door> activeDoors = new List<Door>();

    public bool open;
    public bool close;

    private void Update()
    {
        if (open) { OpenDoors(); open = false; }
        if (close) { CloseDoors(); close = false; }
    }

    //SETUP METHODS
    #region Setup Methods
    public void SetupRoom(Coordinates _coordinates, RoomType _roomType, DungeonGenerator _dungeonGenerator)
    {
        coordinates = _coordinates;
        roomType = _roomType;
        dungeonGenerator = _dungeonGenerator;

        name = $"{coordinates.x}" + $",{coordinates.y}";
        gameObject.SetActive(true);

        childArray = GetComponentsInChildren<Renderer>();

        switch (roomType)
        {
            case RoomType.Normal:
                break;
            case RoomType.End:
                name += " ENDROOM";
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                foreach(Renderer child in childArray)
                {
                    child.material.color = Color.green;
                }
                break;
            case RoomType.Exit:
                name += " EXIT";
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                foreach (Renderer child in childArray)
                {
                    child.material.color = Color.red;
                }

                break;
            case RoomType.Shop:
                name += " SHOP";
                gameObject.GetComponent<Renderer>().material.color = Color.orange;
                foreach (Renderer child in childArray)
                {
                    child.material.color = Color.orange;
                }
                break;
        }
    }

    public void SetupDoors(int[,] dungeon)
    {
        //top
        if (coordinates.y + 1 < dungeon.GetLength(1) && dungeon[coordinates.x,coordinates.y+1] != 0)
        {
            walls[0].SetActive(false);
            doors[0].gameObject.SetActive(true);
            activeDoors.Add(doors[0]);
        }

        //bottom
        if (coordinates.y - 1 >= 0  && dungeon[coordinates.x, coordinates.y - 1] != 0)
        {
            walls[1].SetActive(false);
            doors[1].gameObject.SetActive(true);
            activeDoors.Add(doors[1]);
        }

        //left
        if (coordinates.x - 1 >= 0 && dungeon[coordinates.x - 1, coordinates.y] != 0)
        {
            walls[2].SetActive(false);
            doors[2].gameObject.SetActive(true);
            activeDoors.Add(doors[2]);
        }

        //right
        if (coordinates.x + 1 < dungeon.GetLength(0) && dungeon[coordinates.x + 1, coordinates.y] != 0)
        {
            walls[3].SetActive(false);
            doors[3].gameObject.SetActive(true);
            activeDoors.Add(doors[3]);
        }
    }
    #endregion

    public void OpenDoors()
    {
        foreach(Door door in activeDoors)
        {
            door.Open();
        }

        //also open doors of adjacent rooms
        foreach(Room room in dungeonGenerator.rooms)
        {
            //up
            if (room.coordinates.x == coordinates.x && room.coordinates.y == coordinates.y + 1)
            {
                room.doors[1].door.SetActive(false);
            }

            //down
            if (room.coordinates.x == coordinates.x && room.coordinates.y == coordinates.y - 1)
            {
                room.doors[0].door.SetActive(false);
            }

            //left
            if (room.coordinates.x == coordinates.x - 1 && room.coordinates.y == coordinates.y)
            {
                room.doors[3].door.SetActive(false);
            }

            //right
            if (room.coordinates.x == coordinates.x + 1 && room.coordinates.y == coordinates.y)
            {
                room.doors[2].door.SetActive(false);
            }
        }

    }

    public void CloseDoors()
    {
        foreach (Door door in activeDoors)
        {
            door.Close();
        }
    }


}


