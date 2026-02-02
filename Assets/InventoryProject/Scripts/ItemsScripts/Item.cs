
using GameplayHud;
using UnityEngine;
using Zenject;
public interface ICollectable
{
    bool Collect(InventoryHudController controller);
    Vector3 GetPos();
}
public class Item : MonoBehaviour, ISpawnable, ICollectable
{
    
    public bool isRigged;
    public int id;
    PoolzSystem pool;

    public bool Collect(InventoryHudController controller)
    {
        var value = controller.CollectItem(id);
        if (value)
        {
            Release();
        }
        return value;
    }
    
    public virtual void Attach(Transform parent, Transform character)//, CharacterRiging rigs)
    {
      
    }

    public virtual void InitSpawn(Vector3 position, Quaternion rotation, PoolzSystem pool)
    {
        this.pool = pool;
        transform.position = position + Vector3.up;
        transform.rotation = rotation;

    }


    public void Release()
    {
        pool.Release(SpawnType.prefab, gameObject);
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }
}
