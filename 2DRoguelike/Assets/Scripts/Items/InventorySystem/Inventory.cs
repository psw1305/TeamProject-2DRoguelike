using UnityEngine;

// TODO -> UI 연결해서 현재 보유중인 아이템이 보이도록
public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private ItemBlueprint _currentItem;

    private void Awake()
    {
        Instance = this;
        _currentItem = null;
    }

    public void GetSpecial(ItemBlueprint itemBlueprint)
    {
        InteractableItemBluePrint interactable = itemBlueprint as InteractableItemBluePrint;
        
        if(interactable.interactType == InteractType.Active)
            GetActive(itemBlueprint);
        else
            GetPassive(itemBlueprint);
    }

    private void GetActive(ItemBlueprint itemBlueprint)
    {
        if(_currentItem != null)
        {
            Vector3 playerLoc = Main.Game.Player.gameObject.transform.position;
            Main.Reward.CreateReward(_currentItem, playerLoc);
        }
        _currentItem = itemBlueprint;
    }

    private void GetPassive(ItemBlueprint itemBlueprint)
    {
        EquipManager.Instance.GetPassiveItem(itemBlueprint);
    }

    public void UseActiveItem()
    {
        if(_currentItem == null) return;
        EquipManager.Instance.GetItemStatus(_currentItem);
        _currentItem = null;
    }
}
