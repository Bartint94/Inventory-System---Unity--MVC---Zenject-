using System.Collections.Generic;
using UnityEngine;

public interface ISlotable
{
    public void Toggle(bool value);
    public void InitSlots();
    public void AddDragable(IDragable dragable, int pullableId);
    void RemoveDragable(IDragable dragable);
}
