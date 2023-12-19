using UnityEngine;

public class RewardManager
{
    #region Fields

    private GameObject _pickupBase;
    private GameObject _passiveBase;
    private GameObject _shopBase;

    private ItemBlueprint[] _pickupItems;
    private ItemBlueprint[] _passiveItems;

    #endregion

    public void Initialize()
    {
        _pickupBase = Main.Resource.GetObject("Pickup_Base");
        _passiveBase = Main.Resource.GetObject("Passive_Base");
        _shopBase = Main.Resource.GetObject("Shop_Base");
        _pickupItems = Resources.LoadAll<ItemBlueprint>("ScriptableObjects/Items/Pickup");
        _passiveItems = Main.Resource.GetItemBlueprints("ScriptableObjects/Items/Passive");
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
    /// 상자 보상
    /// </summary>
    /// <param name="parent"></param>
    public void ChestReward(Transform parent)
    {
        var pickupItem = GameObject.Instantiate(_pickupBase, parent).GetComponent<PickupItem>();
        pickupItem.SetItem(GetRandomPickupBlueprint());
        
        // 상자 오픈시, 튀어나오는 연출
        Vector2 force = Random.insideUnitCircle * 30;     
        if (pickupItem.GetComponent<Rigidbody2D>())
        {
            pickupItem.GetComponent<Rigidbody2D>().AddForce(force);
        }
    }

    /// <summary>
    /// 보물방 전시
    /// </summary>
    /// <param name="position"></param>
    public void DisplayTreasures(Vector2 position)
    {
        var interactableItem = GameObject.Instantiate(_passiveBase).GetComponent<InteractableItem>();
        interactableItem.gameObject.transform.position = position;
        interactableItem.SetItem(GetRandomPassiveBlueprint());
    }

    /// <summary>
    /// 상점방 전시
    /// </summary>
    /// <param name="position"></param>
    public void DisplayProducts(Vector2 position)
    {
        float[] x = {-5,0,5};
        for(int i=0; i<3; i++)
        {
            var shopItem = GameObject.Instantiate(_shopBase).GetComponent<ShopItem>();
            shopItem.gameObject.transform.position = position + new Vector2(x[i] , 0);
            shopItem.SetItem(GetRandomPassiveBlueprint());
        }
    }
}
