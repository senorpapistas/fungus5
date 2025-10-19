using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Player")]
    public GameObject player;

    [Header("Currently Open Menu")]
    public MenuClass currentOpenMenu;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(MenuClass menu)
    {
        if (currentOpenMenu != null && currentOpenMenu != menu)
        {
            currentOpenMenu.ForceCloseMenu();
        }

        currentOpenMenu = menu;
    }

    public void CloseMenu(MenuClass menu)
    {
        if (currentOpenMenu == menu)
        {
            currentOpenMenu = null;
        }
    }
}