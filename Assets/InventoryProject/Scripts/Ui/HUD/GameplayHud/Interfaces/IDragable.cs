using GameplayHud;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable
{
    int GetInstanceId { get; }
    int GetDatabaseId { get; }
    void Drag();
    void Drop(bool value);
    void DropOut();
    void ResetOffset();
    List<Vector2Int> GetCoordinates();
    public void SetNewPull(List<PullableSlot> pivot, Transform parent);
    void SetPrevPull();
}

