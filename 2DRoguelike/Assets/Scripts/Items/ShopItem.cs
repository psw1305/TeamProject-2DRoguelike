using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ShopItem : InteractableItem
{
    private void Start()
    {
        InteractableItemBluePrint bluePrint = blueprint as InteractableItemBluePrint;
        GetComponentInChildren<TextMeshProUGUI>().text = "Cost : " + bluePrint.itemCost.ToString();
    }
    public override bool PlayerGetThis()
    {
        if (HasCoin())
            return true;
        return false;
    }
    
    public bool HasCoin()
    {
        InteractableItemBluePrint bluePrint = blueprint as InteractableItemBluePrint;
        return Main.Game.Player.UseCoin(bluePrint.itemCost);
    }
}
