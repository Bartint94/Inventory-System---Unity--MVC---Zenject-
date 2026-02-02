using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EqHudController : MonoBehaviour, ISlotable
{
    [SerializeField] EqHudModel model;

    List<PullableSlot> lastPullable = new List<PullableSlot>();


    public int GetPivotSlotHudId()
    {
        throw new System.NotImplementedException();
    }

    public PullableSlot GetSlot(int id)
    {
        throw new System.NotImplementedException();
    }

    public void InitSlots()
    {
       model.InitSlots();
    }
    public bool IsFreeSlots(List<Vector2Int> checkList, Vector2Int slotPos)
    {
        foreach (var slot in model.slots)
        {
          //  if(slot.pos == slotPos)
            {
                model.lastPullable = slot;  
            //    return slot.isEmpty; 
            }
        }
        return false;
    }


    void ISlotable.AddDragable(IDragable dragable, int id)
    {
     
    }
    public void UpdatePrevSlots(List<PullableSlot> prevSlots, bool isActive)
    {
        foreach (var slot in prevSlots)
        {
         //   slot.Activate(isActive);
        }
    }
    public void Toggle(bool value)
    {
        gameObject.SetActive(value);
    }

    public void RemoveDragable(IDragable dragable)
    {
        throw new System.NotImplementedException();
    }
}
