using UnityEngine;
using UnityEngine.UI;

public class RoomIcon : MonoBehaviour
{
    public Image icon;

    [Header("Doors")]
    public Image up;
    public Image down;
    public Image left;
    public Image right;

    public void ResetIcon()
    {
        if (icon) icon.color = Color.black;
        if (up) up.enabled = false;
        if (down) down.enabled = false;
        if (left) left.enabled = false;
        if (right) right.enabled = false;
    }
}
