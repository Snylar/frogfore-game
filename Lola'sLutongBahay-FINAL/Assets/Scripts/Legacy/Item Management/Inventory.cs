using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private List<ItemManager> itemsList;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        itemsList = new List<ItemManager>();
    }

    public void AddItems(ItemManager item)
    {
        if(item.isStackable)
        {
            bool itemAlreadyInInventory = false;

            foreach(ItemManager itemInInventory in itemsList)
            {
                if(itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            
            if(!itemAlreadyInInventory)
            {
                itemsList.Add(item);
            }
        }
        else
        {
            itemsList.Add(item);
        }
        
    }
    public void RemoveItem(ItemManager item)
    {
        if(item.isStackable)
        {
            ItemManager inventoryItem = null;

            foreach(ItemManager itemInInventory in itemsList)
            {
                if(itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount--;
                    inventoryItem = itemInInventory;
                }
            }

            if(inventoryItem != null & inventoryItem.amount <= 0)
            {
                itemsList.Remove(inventoryItem);
            }
        }
        else
        {
            itemsList.Remove(item);
        }
    }
    public List<ItemManager> GetItemsList()
    {
        return itemsList;
    }

    public bool HasQuestItem(string itemName)
    {
        foreach (ItemManager item in itemsList)
        {
            if (item.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }
}
