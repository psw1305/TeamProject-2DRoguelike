using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSystem
{
    public void GetItemStatus(InteractableItemBluePrint targetBlueprint)
    {
        foreach(StatusContainer data in targetBlueprint.valueList)
            AddStats(data.statusType, data.statusModType, data.value);
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
