using UnityEngine;
using UnityEngine.UI;

//grid-based map representing the dungeon by DungeonGenerator.
public class Map : MonoBehaviour
{
    public GameObject map;

    [Header("Prefabs")]
    public GameObject roomIcon;
    public GameObject playerIcon;

    [Header("Player")]
    public GameObject player;

    public GameObject[,] grid = new GameObject[11,11];

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
                GameObject newMapRoom = Instantiate(roomIcon, map.transform);
                newMapRoom.name = i + " " + j;
                newMapRoom.SetActive(true);
                grid[i, j] = newMapRoom;
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
        Image image = grid[room.coordinates.x, room.coordinates.y].GetComponent<Image>();
        RoomType roomType = room.roomType;

        switch (roomType)
        {
            case RoomType.Normal:
                image.color = Color.white;
                break;
            case RoomType.Start:
                image.color = Color.white;
                break;
            case RoomType.End:
                image.color = Color.green;
                break;
            case RoomType.Exit:
                image.color = Color.red;
                break;
            case RoomType.Shop:
                image.color = Color.orange;
                break;
        }

        playerIcon.transform.SetParent(image.gameObject.transform,true);
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
                grid[i, j].GetComponent<Image>().color = Color.black;
            }
        }
    }
}
