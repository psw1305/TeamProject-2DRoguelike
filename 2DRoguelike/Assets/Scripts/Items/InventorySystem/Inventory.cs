using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // TODO => ItemType.Health
        switch(itemType)
        {
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
        if(_currentItem != null)
        {
            RewardManager.Instance.CreateTargetReward(_currentItem.itemName, new Vector3(0,0,0));
            // TODO => 위치를 플레이어로 변경
        }
        _currentItem = itemBlueprint;
    }

    public void UseSpecial()
    {
        if(_currentItem == null) return;

        // TODO => 아이템 효과에 맞게 플레이어에게 스탯 혹은 기믹 부여
        _currentItem = null;
    }

    public void UseKey()
    {
        Key--;
    }
}
