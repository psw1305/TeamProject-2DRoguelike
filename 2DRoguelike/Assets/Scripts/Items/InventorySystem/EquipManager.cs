using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    // Prototype.
    public static EquipManager Instance;
    List<Sprite> itemSprites;

    private void Awake()
    {
        Instance = this;
        itemSprites = new List<Sprite>();
    }

    public void GetItemStatus(ItemBlueprint itemBlueprint)
    {
        InteractableItemBluePrint targetBp = itemBlueprint as InteractableItemBluePrint;
        foreach(StatusContainer data in targetBp.valueList)
            AddStats(data.statusType, data.statusModType, data.value);
    }

    public void GetPassiveItem(ItemBlueprint itemBlueprint)
    {
        InteractableItemBluePrint targetBp = itemBlueprint as InteractableItemBluePrint;
        GetItemStatus(itemBlueprint);
        itemSprites.Add(targetBp.itemSprite);
    }

    private void AddStats(StatusType type, StatModType modType, float value)
    {
        StatUnit target = GetStatusType(type);
        target.AddModifier(new StatModifier(value, modType));
    }

    private StatUnit GetStatusType(StatusType type)
    {
        switch(type)
        {
            case StatusType.HP : return Main.Game.Player.HP;
            case StatusType.Damage : return Main.Game.Player.Damage;
            case StatusType.Speed : return Main.Game.Player.Speed;
            case StatusType.AttackRange : return Main.Game.Player.AttackRange;
            case StatusType.ShotSpeed : return Main.Game.Player.ShotSpeed;
        }

        Debug.Log("Status Type mismatch");
        return null;
    }
}
