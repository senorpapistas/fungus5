using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private List<Item> itemTable;

    [SerializeField]
    private Item item;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        item = RandomItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            GiveItem(GameObject.FindWithTag("Player"));
            Destroy(gameObject);
        }
    }

    Item RandomItem()
    {
        return itemTable[Random.Range(0, itemTable.Count)];
    }

    public void GiveItem(GameObject player)
    {
        player.GetComponent<Inventory>().AddToInventory(item);
    }
}
