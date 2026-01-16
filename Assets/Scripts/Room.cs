using UnityEngine;

public enum RoomType {Normal,End,Exit,Shop}

[System.Serializable]
public class Room : MonoBehaviour
{
    public Coordinates coordinates;
    public RoomType roomType;

    public void SetupRoom(Coordinates _coordinates, RoomType _roomType)
    {
        coordinates = _coordinates;
        roomType = _roomType;

        name = $"{coordinates.x}" + $",{coordinates.y}";
        gameObject.SetActive(true);

        switch(roomType)
        {
            case RoomType.Normal:
                break;
            case RoomType.End:
                name += " ENDROOM";
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                break;
            case RoomType.Exit:
                name += " EXIT";
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case RoomType.Shop:
                name += " SHOP";
                gameObject.GetComponent<Renderer>().material.color = Color.orange;
                break;
        }
    }
}


