using UnityEngine;

public class PickupItem : BaseItem
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerItemPickup();
        }
    }

    protected virtual void PlayerItemPickup()
    {
        bool isPickup = false;

        switch (blueprint.ItemType)
        {
            case ItemType.Heart:
                isPickup = Main.Game.Player.GetHeart(blueprint.ItemAmount);
                break;
            case ItemType.Coin:
                isPickup = Main.Game.Player.GetCoin(blueprint.ItemAmount);
                break;
            case ItemType.Key:
                isPickup = Main.Game.Player.GetKey(blueprint.ItemAmount);
                break;
            case ItemType.Bomb:
                isPickup = Main.Game.Player.GetBomb(blueprint.ItemAmount);
                break;
        }

        if (isPickup) Destroy(gameObject);
    }
}
