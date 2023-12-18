using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemTable : MonoBehaviour
{
    private Dictionary<string, ItemBlueprint> _itemDict;

    private void Awake()
    {
        LoadItems();
    }
    
    private void LoadItems()
    {
        _itemDict = new Dictionary<string, ItemBlueprint>();
        ItemBlueprint[] itemBlueprints = Resources.LoadAll<ItemBlueprint>("ScriptableObject/Items");
        foreach (ItemBlueprint blueprint in itemBlueprints)
        {
            _itemDict.Add(blueprint.itemName, blueprint);
        }
    }

    public ItemBlueprint GetRandomItem()
    {
        int rand = Random.Range(0, _itemDict.Count);
        return _itemDict.ElementAt(rand).Value;
    }

    public ItemBlueprint GetTargetItem(string name)
    {
        return _itemDict[name];
    }
}
