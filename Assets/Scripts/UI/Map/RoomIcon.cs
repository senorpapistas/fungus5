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

    private void Start()
    {
        //ResetIcon();    
    }

    public void ResetIcon()
    {
        icon.color = Color.black;
        up.enabled = false;
        down.enabled = false;
        left.enabled = false;
        right.enabled = false;
    }
}
