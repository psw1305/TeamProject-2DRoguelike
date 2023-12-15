using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Coin,
    Heart,
    Bomb,
    Key,
    Special // TODO : 후순위
}

public enum HowItemGet
{
    None,
    Automatic,
    Interact // TODO : 후순위
}