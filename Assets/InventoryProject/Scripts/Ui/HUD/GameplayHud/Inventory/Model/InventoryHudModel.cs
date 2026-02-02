using DataBase;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PullReasult { Success, SlotOccupied, Error, WrongItem}


public class InventoryHudModel
{
    public List<InventorySlotModel> slots;
    public List<InventoryItemModel> items = new List<InventoryItemModel>();
    public ItemDataBase dataBase;

    public InventoryHudModel(ItemDataBase dataBase, int rows, int columns)
    {
        this.dataBase = dataBase;

        slots = new List<InventorySlotModel>(rows * columns);
        var id = 0;

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                slots.Add(new InventorySlotModel { id = id, pos = new Vector2Int(j, i), isEmpty = true });
                id++;
            }
        }
    }
    public void RemoveItem(int instanceId)
    {
        var item = items.Find(p => p.instanceId == instanceId);
        if (item == null) return;
        items.Remove(item);
        foreach(var slot in item.occupedSlots)
        {
            slot.isEmpty = true;
        }
    }
    public bool ItemExist(int instanceId)
    { 
        var item = items.Find(p => p.instanceId == instanceId);
        if(item == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public PullReasult TryAddItem(InventoryItemModel item, out List<int> occupiedIds)
    {


        var itemData = dataBase.data[item.databaseId];

        


        foreach (var offset in itemData.Offsets)
        {
            foreach (var slot in slots)
            {
                if (slot.pos == slots[item.pivotSlotId].pos + offset)
                {
                    if(slot.isEmpty)
                    {
                        item.occupedSlots.Add(slot);
                    }
                    else
                    {
                        occupiedIds = null;
                        return PullReasult.SlotOccupied;
                    }
                }
            }
        }
        if (item.occupedSlots.Count == itemData.Offsets.Count)
        {
            occupiedIds = new List<int>();
            foreach(var slot in item.occupedSlots)
            {
                slot.isEmpty = false;   
                occupiedIds.Add(slot.id);
            }
            items.Add(item);

            return PullReasult.Success;
        }
        else
        {
            occupiedIds = null;
            return PullReasult.Error;
        }
    }

    public PullReasult TryMoveItem(List<Vector2Int> checkList, int nextSlotId, int instanceId, out List<int> newSlotIds)
    {

        var item = items.Find(p => p.instanceId == instanceId);
        if (item == null)
        {
            newSlotIds = null;
            return PullReasult.WrongItem;
        }


        List<Vector2Int> convertCheckList = new List<Vector2Int>();

        foreach (var check in checkList)
        {
            convertCheckList.Add(slots[nextSlotId].pos + check);
        }

        var occupedSlots = new List<InventorySlotModel>();
        var ids = new List<int>();
        occupedSlots.Clear();
        

        foreach (var check in convertCheckList)
        {
            foreach (var slot in slots)
            {
                if (slot.pos == check)
                {
                    if (slot.isEmpty || item.occupedSlots.Contains(slot))
                    {
                        occupedSlots.Add(slot);
                    }
                    else
                    {
                        newSlotIds = null;
                        return PullReasult.SlotOccupied;
                    }
                }
            }
        }
        if (occupedSlots.Count == convertCheckList.Count)
        {
            foreach(var slot in item.occupedSlots)
            {
                slot.isEmpty = true;
            }
            foreach (var slot in occupedSlots)
            {
                slot.isEmpty = false;

                ids.Add(slot.id);
            }
            item.occupedSlots = occupedSlots;
            newSlotIds = ids;
            return PullReasult.Success;
        }
        else
        {
            newSlotIds = null;
            return PullReasult.Error;
        }
    }
}
