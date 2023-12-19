using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO -> UI 연결해서 현재 보유중인 아이템이 보이도록
public class Inventory : MonoBehaviour
{
    // Prototype.

    public static Inventory Instance;

    public int Key {get; private set;}
    public int Bomb { get; private set;}
    public int Coin {get; private set;}
    private const int MaxBombStock = 9;
    private const int MaxCoinStock = 999;
    private const int MaxKeyStock = 999;

    private ItemBlueprint _currentItem;

    private void Awake()
    {
        Instance = this;
        // TODO => SaveData Init.
        Key = 0;
        Bomb = 0;
        Coin = 0;
        _currentItem = null;
    }

    public bool GetItem(ItemType itemType, int amount)
    {
        switch(itemType)
        {
            // TODO => ItemType.Health
            case ItemType.Coin : return GetCoin(amount);
            case ItemType.Key : return GetKey(amount);
            case ItemType.Bomb : return GetBomb(amount); 
        }
        return false;
    }

    private bool GetCoin(int amount)
    {
        if(Coin + amount > MaxCoinStock) return false;
        Coin += amount;
        return true;
    }

    private bool GetKey(int amount)
    {
        if(Key + amount > MaxKeyStock) return false;
        Key += amount;
        return true;
    }

    private bool GetBomb(int amount)
    {
        if(Bomb + amount > MaxBombStock) return false;
        Bomb += amount;
        return true;
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
            RewardManager.Instance.CreateReward(_currentItem, playerLoc);
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

    public void UseKey()
    {
        Key--;
    }
}
