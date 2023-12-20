using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ShopItem : InteractableItem
{
    private void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = "Cost : " + targetBp.itemCost.ToString();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        if(!HasCoin()) return;

        _equipSystem.GetItemStatus(targetBp);
        // TODO => 소리
        Destroy(gameObject);
    }
    public bool HasCoin()
    {
        return Main.Game.Player.UseCoin(targetBp.itemCost);
    }
}
