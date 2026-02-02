using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;



namespace GameplayHud
{

    public class InventoryHudController : ISlotable
    {

        SlotsConfig bpConfig;
        InventoryView view;
        InventoryHudModel model;
        Selecter selecter;
        PoolzSystem pool;
        ItemSpawner spawner;
        List<PullableSlot> pullables;
        private int instanceId;

        public InventoryHudController(SlotsConfig bpConfig, InventoryView view, InventoryHudModel model, Selecter selecter, PoolzSystem pool, ItemSpawner spawner)
        {
            this.bpConfig = bpConfig;
            this.view = view;
            this.model = model;
            this.selecter = selecter;
            this.pool = pool;
            this.spawner = spawner;
        }


        public void InitSlots()
        {
            pullables = view.pullableSlots;

            for (int i = 0; i < model.slots.Count; i++)
            {
                pullables[i].id = model.slots[i].id;
                pullables[i].Init(bpConfig.ColorPalette, this);
            }
        }
        void AddNewItem(IDragable dragable ,int pivotId)
        {
            var occupiedIds = new List<int>();

            var item = new InventoryItemModel(pivotId, dragable.GetDatabaseId, dragable.GetInstanceId);
            var result = model.TryAddItem(item, out occupiedIds);
            if (result == PullReasult.Success)
            {
                ViewUpdate(occupiedIds, dragable);
            }
            else
            {
                Debug.Log(result);
            }
        }
        void ViewUpdate(List<int> occupiedIds, IDragable dragable)
        {
            List<PullableSlot> newPullables = new List<PullableSlot>();
            foreach (var id in occupiedIds)
            {
                newPullables.Add(pullables[id]);
            }
            dragable.SetNewPull(newPullables, view.slotsParent);
        }
        public void Load(int pivodId, int databaseId)
        {
            var viewOb = pool.Spawn(SpawnType.ui, databaseId, Vector3.zero, Quaternion.identity);
            ItemHudView view = viewOb.GetComponent<ItemHudView>();
            if (view == null) return;
            InitItem(view, databaseId);
            var dragable = view.GetPivotDragable();

            AddNewItem(dragable,pivodId);
        }

        public void AddDragable(IDragable dragable, int pivotId)
        {
            var exist = model.ItemExist(dragable.GetInstanceId);

            if (exist)
            {
               MoveItem(dragable, pivotId);
            }
            else
            {
                AddNewItem(dragable, pivotId);
            }


        }
        void MoveItem(IDragable dragable, int pivotId)
        {
            var occupiedIds = new List<int>();
            var result = model.TryMoveItem(dragable.GetCoordinates(), pivotId, dragable.GetInstanceId, out occupiedIds);
            Debug.Log(result);
            if (result == PullReasult.Success)
            {
                ViewUpdate(occupiedIds, dragable);

            }
            if (result == PullReasult.SlotOccupied)
            {
                dragable.SetPrevPull();
            }
        }
        public void Toggle(bool value)
        {
            if (value)
            {
                view.Activate();
            }
            else
            {
                view.Deactivate();
            }
        }

        void InitItem(ItemHudView item, int databaseId)
        {
            item.InitInventory(instanceId, this);
            item.InitData(databaseId);
            instanceId++;
        }
        public void RemoveDragable(IDragable dragable)
        {
            model.RemoveItem(dragable.GetInstanceId);
            spawner.SpawnFromInventory(dragable.GetDatabaseId);
        }

        internal bool CollectItem(int databaseId)
        {
            var spawn = pool.Spawn(SpawnType.ui, databaseId, Vector3.zero, Quaternion.identity);
            var itemView = spawn.GetComponent<ItemHudView>();

            if (itemView != null)
            {
                Toggle(true);
                InitItem(itemView, databaseId);
                itemView.DragPivot(selecter);
                return true;
            }
            else
            { 
                return false;
            }
        }
        public bool IsDragging()
        {
            return selecter.hadDragable;
        }
    }
}

