using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//grid-based map representing the dungeon by DungeonGenerator.
public class Map : MonoBehaviour
{
    public GameObject map;

    [Header("Prefabs")]
    public RoomIcon roomIcon;
    public GameObject playerIcon;

    [Header("Player")]
    public GameObject player;

    public RoomIcon[,] grid = new RoomIcon[11,11];

    private void OnEnable()
    {
        DungeonPlayerTracker.RoomChangeEvent += UpdateMap;
        DungeonGenerator.DungeonGeneratedEvent += ResetMap;

    }

    private void OnDisable()
    {
        DungeonPlayerTracker.RoomChangeEvent -= UpdateMap;
        DungeonGenerator.DungeonGeneratedEvent += ResetMap;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                RoomIcon newRoomIcon = Instantiate(roomIcon, map.transform);
                newRoomIcon.name = i + " " + j;
                newRoomIcon.gameObject.SetActive(true);
                grid[i, j] = newRoomIcon;
            }
        }

        playerIcon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayerIcon();
    }

    void UpdateMap(Room room)
    {
        Image icon = grid[room.coordinates.x, room.coordinates.y].icon;
        RoomType roomType = room.roomType;

        switch (roomType)
        {
            case RoomType.Normal:
                icon.color = Color.white;
                break;
            case RoomType.Start:
                icon.color = Color.white;
                break;
            case RoomType.End:
                icon.color = Color.green;
                break;
            case RoomType.Exit:
                icon.color = Color.red;
                break;
            case RoomType.Shop:
                icon.color = Color.orange;
                break;
        }

        if (room.hasUpDoor)
        {
            grid[room.coordinates.x, room.coordinates.y].up.enabled = true;
        }
        if (room.hasDownDoor)
        {
            grid[room.coordinates.x, room.coordinates.y].down.enabled = true;
        }
        if (room.hasLeftDoor)
        {
            grid[room.coordinates.x, room.coordinates.y].left.enabled = true;
        }
        if (room.hasRightDoor)
        {
            grid[room.coordinates.x, room.coordinates.y].right.enabled = true;
        }

        playerIcon.transform.SetParent(icon.gameObject.transform,true);
        playerIcon.transform.localPosition = new Vector3(0, 0, 0);
    }

    void RotatePlayerIcon()
    {
        playerIcon.transform.rotation = Quaternion.Euler(0, 0, -player.transform.rotation.eulerAngles.y);
    }

    void ResetMap()
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j].ResetIcon();
            }
        }
    }
}
