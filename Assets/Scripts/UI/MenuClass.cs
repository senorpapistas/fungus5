using System.Collections;
using UnityEngine;

public class MenuClass : MonoBehaviour
{
    public GameObject menu;
    public bool isActive;

    private void Start()
    {
        isActive = false;
        menu.SetActive(false);
    }

    public void ToggleMenu()
    {
        if (!isActive)
        {
            if (UIManager.Instance.currentOpenMenu == null || UIManager.Instance.currentOpenMenu != this)
            {
                OpenMenu();
            }
        }
        else
        {
            CloseMenu();
        }
    }

    private void OpenMenu()
    {
        isActive = true;
        menu.SetActive(true);

        UIManager.Instance.OpenMenu(this);
    }

    private void CloseMenu()
    {
        isActive = false;
        menu.SetActive(false);

        UIManager.Instance.CloseMenu(this);
    }

    public void ForceCloseMenu()
    {
        isActive = false;
        menu.SetActive(false);
    }
}