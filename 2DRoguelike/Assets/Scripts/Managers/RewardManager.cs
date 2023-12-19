using UnityEngine;

public class RewardManager
{
    #region Fields

    private GameObject _item;
    private ItemBlueprint[] _pickupItems;
    private ItemBlueprint[] _passiveItems;

    #endregion

    public void Initialize()
    {
        _item = Main.Resource.GetObject("Pickup_Base");
        _pickupItems = Main.Resource.GetItemBlueprints("ScriptableObjects/Items/Pickup");
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

    /// <summary>
    /// 상자 보상
    /// </summary>
    /// <param name="parent"></param>
    public void ChestReward(Transform parent)
    {
        var pickupItem = GameObject.Instantiate(_item, parent).GetComponent<PickupItem>();
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
    }

    /// <summary>
    /// 상점방 전시
    /// </summary>
    /// <param name="position"></param>
    public void DisplayProducts(Vector2 position)
    {
    }
}
