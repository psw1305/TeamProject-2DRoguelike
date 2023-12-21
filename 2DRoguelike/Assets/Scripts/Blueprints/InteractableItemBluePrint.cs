using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableItem", menuName = "Blueprint/InteractableItem")]
public class InteractableItemBluePrint : ItemBlueprint
{
    public int itemCost;
    [Header("Interact Type")]
    public InteractType interactType;

    [Header("Status Add")]
    public Sprite itemSprite;
    public List<StatusContainer> valueList;
}

[Serializable]
public class StatusContainer
{
    public StatusType statusType;
    public StatModType statusModType;
    public float value;
}

public enum StatusType
{
    None,
    HP,
    Damage,
    Speed,
    AttackRange,
    AttackSpeed,
    ShotSpeed
}

public enum InteractType
{
    None,
    Active,
    Passive
}