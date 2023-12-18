using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableItem", menuName = "Blueprint/InteractableItem")]
public class InteractableItemBluePrint : ItemBlueprint
{
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
    public StatusAddType statusAddType;
    public float value;
}

public enum StatusType
{
    None,
    HP,
    Damage,
    Speed,
    AttackRange,
    ShotSpeed
}

public enum StatusAddType
{
    None,
    Add,
    Subtract,
    Multiply,
    Divide
}

public enum InteractType
{
    None,
    Active,
    Passive
}