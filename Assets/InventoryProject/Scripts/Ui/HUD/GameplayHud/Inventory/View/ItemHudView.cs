
using GameplayHud;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemHudView : MonoBehaviour, ISpawnable
{
    public int id { get; internal set; }
    int instanceId;

    PoolzSystem pool;

    IDragable pivotDragableSlot;
    ISlotable slotable;
    public IDragable GetPivotDragable() => pivotDragableSlot;
    
    List<DragableSlot> dragableSlots = new List<DragableSlot>();
    public List<PullableSlot> pullableSlots = new List<PullableSlot>();

    public Vector3 prevPos;

    public void InitData(int id)
    {
        this.id = id;
    }
    public void InitSpawn(Vector3 position, Quaternion rotation, PoolzSystem pool)
    {
        this.pool = pool;
        var slots = GetComponentsInChildren<DragableSlot>();
        dragableSlots = slots.ToList();
        pivotDragableSlot = dragableSlots[0];
    }
    public void InitInventory(int instanceId, ISlotable slotable)
    {
        this.instanceId = instanceId;
        this.slotable = slotable;
    }
    public int GetInstanceId()
    {
        return instanceId;
    }
    public void SetCurrentPullablesVisualState(SlotVisualState state)
    {
        if (pullableSlots.Count > 0)
            foreach (var slot in pullableSlots)
            {
                slot.SetColor(state);
            }
    }
    void SetPullableSlots(List<PullableSlot> slots)
    {
        this.pullableSlots.Clear();
        this.pullableSlots = slots;
    }

    void SetNewTransform(Vector3 pos, Transform parent)
    {
        transform.position = pos;
        transform.SetParent(parent,true);
        transform.localScale = Vector3.one;

    }
    public void SetNewAttachment(List<PullableSlot> slots, Transform parent)
    {
        SetPullableSlots(slots);
        SetCurrentPullablesVisualState(SlotVisualState.inactive);
        SetNewTransform(slots[0].transform.position, parent);
    }
    public void SetPrevAttachment()
    {
        SetCurrentPullablesVisualState(SlotVisualState.inactive);
        transform.position = prevPos;
    }     
    public void DragPivot(Selecter selecter)
    {
        pivotDragableSlot.Drag();
        selecter.SetDragableOutside(pivotDragableSlot);
        pivotDragableSlot.ResetOffset();
    }


    public void Release()
    {
        foreach(var slot in dragableSlots)
        {
            slot.isDragging = false;
        }
        pullableSlots.Clear();
        pool.Release(SpawnType.ui, this.gameObject);
    }

    internal void DropOut()
    {
        slotable.RemoveDragable(pivotDragableSlot);
        Release();
    }

    internal List<Vector2Int> GetCoordinates(Vector2Int pos)
    {
        var convertCoordinatesList = new List<Vector2Int>();
        foreach (var check in dragableSlots)
        {
            convertCoordinatesList.Add(check.pos - pos);
        }
        return convertCoordinatesList;
    }

    internal int GetDatabaseId()
    {
        return id;
    }
}
