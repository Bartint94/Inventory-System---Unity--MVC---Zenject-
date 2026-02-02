using System.Collections.Generic;
using UnityEngine;

public class InventoryItemModel
{
    public int instanceId { get; }
    public int databaseId { get; }
    public int pivotSlotId;
    public List<InventorySlotModel> occupedSlots = new List<InventorySlotModel>();

    public InventoryItemModel(int pivotSlotId, int databaseId, int instanceId)
    {
        this.pivotSlotId = pivotSlotId;
        this.databaseId = databaseId;
        this.instanceId = instanceId;
    }

}
