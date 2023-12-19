using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemTable : MonoBehaviour
{
    private Dictionary<string, ItemBlueprint> _pickupTable;
    private Dictionary<string, ItemBlueprint> _activeTable;
    private Dictionary<string, ItemBlueprint> _passiveTable;

    private void Awake()
    {
        LoadTables();
    }
    
    private void LoadTables()
    {
        LoadPickupItems();
        LoadActiveItems();
        LoadPassiveItems();
    }

    private void LoadPickupItems()
    {
        _pickupTable = new Dictionary<string, ItemBlueprint>();
        ItemBlueprint[] itemBlueprints = Resources.LoadAll<ItemBlueprint>("ScriptableObject/Items/Pickup");
        foreach (ItemBlueprint blueprint in itemBlueprints)
        {
            _pickupTable.Add(blueprint.itemName, blueprint);
        }
    }

    private void LoadActiveItems()
    {
        _activeTable = new Dictionary<string, ItemBlueprint>();
        ItemBlueprint[] itemBlueprints = Resources.LoadAll<ItemBlueprint>("ScriptableObject/Items/Active");
        foreach (ItemBlueprint blueprint in itemBlueprints)
        {
            _activeTable.Add(blueprint.itemName, blueprint);
        }
    }

    private void LoadPassiveItems()
    {
        _passiveTable = new Dictionary<string, ItemBlueprint>();
        ItemBlueprint[] itemBlueprints = Resources.LoadAll<ItemBlueprint>("ScriptableObject/Items/Passive");
        foreach (ItemBlueprint blueprint in itemBlueprints)
        {
            _passiveTable.Add(blueprint.itemName, blueprint);
        }
    }
    public ItemBlueprint GetTargetItem(string name)
    {
        ItemBlueprint itemBlueprint;
        if(_pickupTable.TryGetValue(name, out itemBlueprint)) return itemBlueprint;
        if(_activeTable.TryGetValue(name, out itemBlueprint)) return itemBlueprint;
        if(_passiveTable.TryGetValue(name, out itemBlueprint)) return itemBlueprint;

        Debug.Log("해당 이름에 맞는 아이템이 없습니다.");
        return null;
    }

    public ItemBlueprint GetRandomPickupItem()
    {
        int rand = Random.Range(0, _pickupTable.Count);
        return _pickupTable.ElementAt(rand).Value;
    }

    public ItemBlueprint GetRandomActiveItem()
    {
        int rand = Random.Range(0, _activeTable.Count);
        return _activeTable.ElementAt(rand).Value;
    }

    public ItemBlueprint GetRandomPassiveItem()
    {
        int rand = Random.Range(0, _passiveTable.Count);
        return _passiveTable.ElementAt(rand).Value;
    }


}
