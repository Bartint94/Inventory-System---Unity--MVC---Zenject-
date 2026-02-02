using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
namespace DataBase
{

    [CreateAssetMenu(fileName = "ItemDataBase", menuName = "ItemDataBase")]
    public class ItemDataBase : ScriptableObject
    {
        [SerializeField] List<ItemData> _data;
        public List<ItemData> data => _data;

        private void OnValidate()
        {
            UpdateIds();
        }

        [ContextMenu("UpdateIds")]
        public void UpdateIds()
        {
            int i = 0;
            foreach (ItemData item in _data)
            {
                item.InitIds(i);

                i++;
            }
        }
    }

}