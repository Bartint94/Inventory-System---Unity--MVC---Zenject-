using UnityEngine;

public class ItemSpawner
{
    PoolzSystem pool;

    public ItemSpawner(PoolzSystem pool )
    {
        this.pool = pool;
    }
   public void SpawnFromInventory(int databaseId)
    {
        Transform camTransform = Camera.main.transform;
        Vector3 spawnPos = camTransform.position + camTransform.forward;
        pool.Spawn(SpawnType.prefab, databaseId, spawnPos, Quaternion.identity);
    }
}
