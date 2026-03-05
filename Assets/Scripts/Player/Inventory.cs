using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> itemList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            UseItems();
        }
    }

    private void UseItems()
    {
        foreach(Item item in itemList)
        {
            item.Use(this.gameObject);
        }
    }

    public void AddToInventory(Item item)
    {
        itemList.Add(item);
        item.Use(this.gameObject);
    }
}
