using UnityEngine;
using GameplayHud;
using System.Collections.Generic;

using UnityEngine.UI;

public class PullableSlot : MonoBehaviour, IPullable
{
    ISlotable slotable;
    public int id;
    Image img;
    SlotColorPalette colorPalette;

    public void Pull(IDragable dragable)
    {
        slotable.AddDragable(dragable, id);
        // dragable.Drop(value);
    }
    public void Init(SlotColorPalette colorPalette, ISlotable slotable)
    {
        img = GetComponent<Image>();
        this.slotable = slotable;
        this.colorPalette = colorPalette;
        SetColor(SlotVisualState.active);
    }
    public void SetColor(SlotVisualState color)
    {
        if (color == SlotVisualState.inactive)
        {
            img.color = colorPalette.inactive;
        }
        if (color == SlotVisualState.active)
        {
            img.color = colorPalette.active;
        }
    }
}

