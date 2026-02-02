using GameplayHud;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
[RequireComponent(typeof(SphereCollider))]
public class ItemDetector : MonoBehaviour
{
    public List<ICollectable> itemsDetected = new List<ICollectable>();
    SphereCollider _collider;
    [Inject]
    Ui3dController ui3dController;
    [Inject]
    InventoryHudController inventoryHudController;
    [Range(1f, 3f)]
    [SerializeField] float radius = 2.5f;


    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
        _collider.radius = radius;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
            itemsDetected.Add(collectable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
            if (itemsDetected.Contains(collectable))
            {
                itemsDetected.Remove(collectable);
            }
        }
    }


    private void Update()
    {
        if (inventoryHudController.IsDragging()) return;

        if (itemsDetected.Count > 0)
        {
            var dis = 0f;
            var bestDist = float.MaxValue;
            ICollectable closest = null;
            foreach (var item in itemsDetected)
            {
                dis = (item.GetPos() - transform.position).sqrMagnitude;
                if (dis < bestDist)
                {
                    bestDist = dis;
                    closest = item;
                }
            }

            ui3dController.ActivatePickUp(closest.GetPos());

            if (Input.GetKeyDown(KeyCode.E) && closest != null)
            {

                ui3dController.DeactivatePickUp();
                var value = closest.Collect(inventoryHudController);
                if (value)
                    itemsDetected.Remove(closest);
            }
        }
        else
        {
        }

    }
}
