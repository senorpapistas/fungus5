using UnityEngine;

[System.Serializable]
public class Room
{
    public int x = 0;
    public int y = 0;

    public bool top;
    public bool bottom;
    public bool left;
    public bool right;

    public Room topRoom;
    public Room bottomRoom;
    public Room leftRoom;
    public Room rightRoom;
}


