using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemTable : MonoBehaviour
{
    private Dictionary<string, ItemBlueprint> _activeTable;

    private void Awake()
    {
        LoadTables();
    }
    
    private void LoadTables()
    {
        LoadActiveItems();
    }

    private void LoadActiveItems()
    {
        _activeTable = new Dictionary<string, ItemBlueprint>();
        ItemBlueprint[] itemBlueprints = Resources.LoadAll<ItemBlueprint>("ScriptableObject/Items/Active");
        foreach (ItemBlueprint blueprint in itemBlueprints)
        {
            _activeTable.Add(blueprint.ItemName, blueprint);
        }
    }

    //public ItemBlueprint GetTargetItem(string name)
    //{
    //    ItemBlueprint itemBlueprint;
    //    if(_pickupTable.TryGetValue(name, out itemBlueprint)) return itemBlueprint;
    //    if(_activeTable.TryGetValue(name, out itemBlueprint)) return itemBlueprint;
    //    if(_passiveTable.TryGetValue(name, out itemBlueprint)) return itemBlueprint;

    //    Debug.Log("해당 이름에 맞는 아이템이 없습니다.");
    //    return null;
    //}

    //public ItemBlueprint GetRandomPickupItem()
    //{
    //    int rand = Random.Range(0, _pickupTable.Count);
    //    return _pickupTable.ElementAt(rand).Value;
    //}
}
