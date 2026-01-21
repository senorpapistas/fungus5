using UnityEngine;

[System.Serializable]
public class Door : MonoBehaviour
{
    public GameObject door;

    public void Open()
    {
        door.SetActive(false);
    }

    public void Close()
    {
        door.SetActive(true);
    }
}
