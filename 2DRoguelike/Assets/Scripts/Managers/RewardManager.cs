using UnityEngine;

public class RewardManager
{
    #region Fields

    private GameObject _pickupFrame;
    private GameObject _interactableFrame;
    private GameObject _chestFrame;
    private ItemTable _itemTable;

    #endregion

    public void Initialize()
    {
        _itemTable = GameObject.FindObjectOfType<ItemTable>();
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

    public void CreateReward(ItemBlueprint targetBlueprint, Vector3 position)
    {
        GameObject targetFrame = SetFrame(targetBlueprint.ItemType);
        BaseItem baseItem = GameObject.Instantiate(targetFrame, position, Quaternion.identity).GetComponent<BaseItem>();
        baseItem.SetItem(targetBlueprint);
    }

    public void CreateBasicReward(Vector3 position)
    {
        ItemBlueprint targetBlueprint;

        int random = Random.Range(1,101);
        if(random >= 10) targetBlueprint = _itemTable.GetRandomPickupItem();
        else targetBlueprint = _itemTable.GetRandomActiveItem();

        CreateReward(targetBlueprint, position);
    }

    public void CreateTreasureReward(Vector3 position)
    {
        ItemBlueprint targetBlueprint = _itemTable.GetRandomPassiveItem();
        CreateReward(targetBlueprint, position);
    }

    public void CreateShopItem(Vector3 position)
    {
        // TODO => 아이템 구매 기능
        ItemBlueprint targetBlueprint;

        int random = Random.Range(1,101);
        if(random <= 20) targetBlueprint = _itemTable.GetRandomPassiveItem();
        else targetBlueprint = _itemTable.GetRandomActiveItem();

        CreateReward(targetBlueprint, position);
    }

    public void CreateRand1()
    {
        int rand = Random.Range(-10,10);
        CreateTreasureReward(new Vector3(rand,rand,0));
    }

    public void CreateRand2()
    {
        int rand = Random.Range(-10,10);
        CreateBasicReward(new Vector3(rand,rand,0));
    }


}
