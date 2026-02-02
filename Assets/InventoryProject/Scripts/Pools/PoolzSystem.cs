using DataBase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;
public enum SpawnType {vfx, prefab, ui}
public interface ISpawnable
{ 
    void InitSpawn(Vector3 position, Quaternion rotation, PoolzSystem pool);
    void Release();
}

public class PoolzSystem : MonoBehaviour
{
    public List<Poolz> poolz = new List<Poolz>();
    public List<Poolz> dataBasePrefabPool;
    public List<Poolz> dataBaseUiPool;
    public ItemDataBase itemDataBase;

    private void Awake()
    {
        foreach (var spawner in poolz)
        {
            InitPool(spawner);
        }
        foreach (var data in itemDataBase.data)
        {
            var prefPool = new Poolz(data.prefab.gameObject, 1);
            InitPool(prefPool);
            dataBasePrefabPool.Add(prefPool);
            var uiPool = new Poolz(data.uiPrefab.gameObject, 1);
            InitPool(uiPool);
            dataBaseUiPool.Add(uiPool);

        }
    }
   
    void InitPool(Poolz spawner)
    {
        spawner._pool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(spawner.prefab);

        },
         ob =>
         {
             ob.SetActive(true);
         },
         ob =>
         {
             ob.SetActive(false);
         },
         ob =>
         {
             Destroy(ob);
         }, false, 5, 5);
    }

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        var pool = poolz.Find(p => p.prefab.name == prefab.name);

        if (pool != null)
        {
            var p = pool.Spawn();
            var spawnable = p.GetComponent<ISpawnable>();
            spawnable.InitSpawn(position, rotation, this);
            return p;
        }
        else
        {
            Debug.Log("pools dont have this object");
            return null;
        }
    }
    public GameObject Spawn(SpawnType type, int dataBaseId, Vector3 position, Quaternion rotation)
    {
        Poolz pool;
        if (SpawnType.prefab == type)
            pool = dataBasePrefabPool[dataBaseId];
        else
        {
            pool = dataBaseUiPool[dataBaseId];
        }
        if (pool != null)
        {
            var p = pool.Spawn();
            var spawnable = p.GetComponent<ISpawnable>();
            spawnable.InitSpawn(position, rotation, this);
            return p;
        }
        else
        {
            Debug.Log("pools dont have this object");
            return null;
        }
    }

    public void Release(GameObject prefab)
    {
        var name = prefab.name.Substring(0, prefab.name.Length - 7);
        var pool = poolz.Find(p => p.prefab.name == name);

        if (pool != null)
        {
            pool.Release(prefab);
        }
        else
        {

            Debug.Log($"pools dont have this object {name}");
        }
    }
    public void Release(SpawnType type, GameObject prefab)
    {
        Poolz pool = null;
        var name = prefab.name.Substring(0, prefab.name.Length - 7);
        if(type == SpawnType.prefab)
        {
            pool = dataBasePrefabPool.Find(p => p.prefab.name == name);
        }
        if(type == SpawnType.ui)
        {

            pool = dataBaseUiPool.Find(p => p.prefab.name == name);
        }
        if(type == SpawnType.vfx)
        {
            pool = poolz.Find(p => p.prefab.name == name);
        }

        if (pool != null)
        {
            pool.Release(prefab);
        }
        else
        {

            Debug.Log($"pools dont have this object {name}");
        }
    }

}

[System.Serializable]
public class Poolz
{

    public GameObject prefab;
    public ObjectPool<GameObject> _pool;
    
    [SerializeField] private int spawnAmount;

    public Poolz(GameObject prefab, int amount)
    {
        this.prefab = prefab;
        spawnAmount = amount;
    }

    public GameObject Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            var spawn = _pool.Get();
       
            return spawn;
        }
        return null;
    }

    public void Release(GameObject blood)
    {
        _pool.Release(blood);
    }

}