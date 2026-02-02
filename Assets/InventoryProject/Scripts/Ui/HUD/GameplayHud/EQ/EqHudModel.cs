using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class EqHudModel : MonoBehaviour
{
    public List<PullableSlot> slots;
    public PullableSlot lastPullable;
    [SerializeField] Color _active;
    [SerializeField] Color _deactive;
    public Color GetActive()
    {
        return _active;
    }
    public Color GetDeactive()
    {
        return _deactive;
    }
    public void InitSlots()
    {
        var slotsArr = GetComponentsInChildren<PullableSlot>();
        slots = slotsArr.ToList();

        //SlotableLogic.InitPullables(1,3, slots);
    }
}
