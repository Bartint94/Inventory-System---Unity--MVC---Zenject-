using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemsData", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Item _prefab;
    [SerializeField] private ItemHudView _uiPrefab;
    [SerializeField] private List<Vector2Int> offsets;
    public Item prefab => _prefab;
    public ItemHudView uiPrefab => _uiPrefab;
    public List<Vector2Int> Offsets => offsets;
    public void InitIds(int id)
    {
        this.id = id;
        prefab.id = id;
        uiPrefab.InitData(id);
    }

}
