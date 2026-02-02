using GameplayHud;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DragableSlot : MonoBehaviour, IDragable
{
    public ItemHudView itemHud;

    public List<Vector2Int> convertCoordinatesList = new List<Vector2Int>();


    Transform parentTransform => itemHud.transform;

    public int GetInstanceId => itemHud.GetInstanceId();

    public int GetDatabaseId => itemHud.GetDatabaseId();

    Vector3 offset;
    public Vector2Int pos;
    internal bool isDragging;

    private void Awake()
    {
        itemHud = GetComponentInParent<ItemHudView>();
    }
    public void Drag()
    {
        itemHud.SetCurrentPullablesVisualState(SlotVisualState.active);

        itemHud.prevPos = parentTransform.position;
        isDragging = true;

        offset = parentTransform.position - Input.mousePosition;
    }

    private void Update()
    {
        if (isDragging)
        {
            parentTransform.transform.position = Input.mousePosition + offset;
        }
    }
    public void Drop(bool value)
    {
        isDragging = false;
    }

    public List<Vector2Int> GetCoordinates()
    {
        return itemHud.GetCoordinates(pos);
    }

    public void DropOut()
    {
        itemHud.DropOut();
    }

    public void SetPrevPull()
    {
        isDragging = false;
        itemHud.SetPrevAttachment();
    }

    public void SetNewPull(List<PullableSlot> pullables, Transform parent)
    {
        isDragging = false;
        itemHud.SetNewAttachment(pullables, parent);
    }

    public void ResetOffset()
    {
        offset = Vector3.zero;
    }
}
