using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private List<ItemManager> itemsList;

    private string filePath = "/player-inventory.json";
    private IDataService dataService = new JsonDataService();
    private bool encryptedEnabled;
    private long savetime;
    private long loadtimme;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        itemsList = new List<ItemManager>();
    }

    public void Save_SerializeJson()
    {
        long start_time = DateTime.Now.Ticks;
        if (dataService.SaveData(filePath, itemsList, encryptedEnabled))
        {
            savetime = DateTime.Now.Ticks - start_time;
            Debug.Log($"Inventory saved ({savetime / 1000f})");
        }
        else
        {
            Debug.LogError("Could not save file! show something on the UI about it!");
        }
    }
    public void Load()
    {
        long start_time = DateTime.Now.Ticks;
        try
        {
            List<ItemManager> data = dataService.LoadData<List<ItemManager>>(filePath, encryptedEnabled);
            loadtimme = DateTime.Now.Ticks - start_time;
            Debug.Log($"Inventory loaded ({loadtimme / 1000f})");
        }
        catch (Exception e)
        {
            Debug.LogError($"Inventory Could not read file!");
        }
    }
    public void AddItems(ItemManager item)
    {
        if (item.isStackable)
        {
            bool itemAlreadyInInventory = false;

            foreach (ItemManager itemInInventory in itemsList)
            {
                if (itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }

            if (!itemAlreadyInInventory)
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
        if (item.isStackable)
        {
            ItemManager inventoryItem = null;

            foreach (ItemManager itemInInventory in itemsList)
            {
                if (itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount--;
                    inventoryItem = itemInInventory;
                }
            }

            if (inventoryItem != null & inventoryItem.amount <= 0)
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
