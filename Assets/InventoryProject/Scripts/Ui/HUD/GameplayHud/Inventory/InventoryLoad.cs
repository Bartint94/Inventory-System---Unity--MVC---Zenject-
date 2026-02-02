using DataBase;
using GameplayHud;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventoryLoad 
{
    InventoryHudController inventory;
    ItemDataBase itemDatabase;
    List<InventoryItemSaveData> save = new List<InventoryItemSaveData>();
    int instanceId = 0;
    public InventoryLoad(InventoryHudController inventory, ItemDataBase itemDataBase)
    {
        this.inventory = inventory;
        this.itemDatabase = itemDataBase;
    }
    public class InventoryItemSaveData
    {
        public int id;
        public int pivotSlotId;

        public InventoryItemSaveData(int id, int pivotSlotId)
        {
            this.id = id;
            this.pivotSlotId = pivotSlotId;
        }
    }
    public void SaveItem(int pivot, int databaseId)
    {
        save.Add(new InventoryItemSaveData(databaseId, pivot));
    }

}

