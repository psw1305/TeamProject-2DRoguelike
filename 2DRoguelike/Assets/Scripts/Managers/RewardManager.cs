using UnityEngine;

public class RewardManager
{
    #region Fields

    private GameObject _pickupBase;
    private GameObject _treasureBase;
    private GameObject _shopBase;

    private ItemBlueprint[] _pickupItems;
    private ItemBlueprint[] _passiveItems;

    private GameObject _normalChest;
    private GameObject _goldenChest;

    #endregion

    public void Initialize()
    {
        _pickupBase = Main.Resource.GetObject("Pickup_Base");
        _treasureBase = Main.Resource.GetObject("Treasure_Base");
        _shopBase = Main.Resource.GetObject("Shop_Base");
        _pickupItems = Main.Resource.GetItemBlueprints("ScriptableObjects/Items/Pickup");
        _passiveItems = Main.Resource.GetItemBlueprints("ScriptableObjects/Items/Passive");

        _normalChest = Main.Resource.GetObject("Pickup_Chest");
        _goldenChest = Main.Resource.GetObject("Pickup_GoldenChest");
    }

    /// <summary>
    /// 랜덤으로 픽업 아이템 드랍
    /// </summary>
    /// <returns>랜덤 픽업 아이템</returns>
    public ItemBlueprint GetRandomPickupBlueprint()
    {
        int rand = Random.Range(0, _pickupItems.Length);
        return _pickupItems[rand];
    }

    public ItemBlueprint GetRandomPassiveBlueprint()
    {
        int rand = Random.Range(0, _passiveItems.Length);
        return _passiveItems[rand];
    }

    /// <summary>
    /// 랜덤 상자 생성
    /// </summary>
    /// <param name="container"></param>
    public void GenerateReward(Vector2 position, Transform container)
    {
        GameObject chest;

        if (Random.Range(1, 101) <= 50)
        {
            chest = GameObject.Instantiate(_normalChest);
        }
        else
        {
            chest = GameObject.Instantiate(_goldenChest);
        }

        chest.transform.SetParent(container);
        chest.transform.localPosition = position;
    }

    /// <summary>
    /// 랜덤 픽업 아이템 드랍
    /// </summary>
    /// <param name="container"></param>
    public void PickupItemDrop(Vector2 position, Transform container, float force = 30)
    {
        var pickupItem = GameObject.Instantiate(_pickupBase, position, Quaternion.identity).GetComponent<PickupItem>();
        pickupItem.transform.SetParent(container);
        pickupItem.SetItem(GetRandomPickupBlueprint());
        
        // 상자 오픈시, 튀어나오는 연출
        Vector2 knockbackForce = Random.insideUnitCircle * force;     
        if (pickupItem.GetComponent<Rigidbody2D>())
        {
            pickupItem.GetComponent<Rigidbody2D>().AddForce(knockbackForce);
        }
    }

    /// <summary>
    /// 보물방 전시
    /// </summary>
    /// <param name="position"></param>
    public void DisplayTreasures(Vector2 position)
    {
        var interactableItem = GameObject.Instantiate(_treasureBase).GetComponent<InteractableItem>();
        interactableItem.gameObject.transform.position = position;
        interactableItem.SetItem(GetRandomPassiveBlueprint());
    }

    /// <summary>
    /// 상점방 전시
    /// </summary>
    /// <param name="position"></param>
    public void DisplayProducts(Vector2 position)
    {
        float x = 2.5f;

        for (int i = 0; i < 5; i++)
        {
            var shopItem = GameObject.Instantiate(_shopBase).GetComponent<ShopItem>();
            shopItem.gameObject.transform.position = position + new Vector2((i * x) - 5, 0);
            shopItem.SetItem(GetRandomPassiveBlueprint());
        }
    }
}
