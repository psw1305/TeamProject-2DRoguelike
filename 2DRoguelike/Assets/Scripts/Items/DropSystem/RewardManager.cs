using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    // Prototype.
    public static RewardManager Instance;
    [SerializeField] private GameObject _pickupFrame;
    [SerializeField] private GameObject _interactableFrame;
    [SerializeField] private GameObject _chestFrame;

    private ItemTable _itemTable;

    private void Awake()
    {
        Instance = this;
        _itemTable = GetComponent<ItemTable>();
    }

    public void CreateTargetReward(string name, Vector3 position)
    {
        ItemBlueprint targetBlueprint = _itemTable.GetTargetItem(name);
        GameObject targetFrame = SetFrame(targetBlueprint.itemType);

        BaseItem baseItem = Instantiate(targetFrame, position, Quaternion.identity).GetComponent<BaseItem>();
        baseItem.SetItem(targetBlueprint);
    }

    public void CreateRandomReward(int quantity, Vector3 position)
    {
        for(int i=0; i < quantity; i++)
        {
            ItemBlueprint targetBluePrint = _itemTable.GetRandomItem();
            CreateTargetReward(targetBluePrint.itemName, position);
        }
    }

    private GameObject SetFrame(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.Coin : case ItemType.Heart :
            case ItemType.Bomb : case ItemType.Key : 
                return _pickupFrame;
            case ItemType.Chest :
                return _chestFrame;
            case ItemType.Special :
                return _interactableFrame;
        }
        return null;
    }

    public void testClick()
    {
        int x = Random.Range(-10,10);
        Vector3 position = new Vector3 (x,x,0);
        CreateRandomReward(1, position);
    }

}
