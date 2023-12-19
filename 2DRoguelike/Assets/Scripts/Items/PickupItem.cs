using UnityEngine;

public class PickupItem : BaseItem
{
    [SerializeField] private ItemBlueprint blueprint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerItemPickup();
        }
    }

    protected virtual void PlayerItemPickup()
    {
        switch (blueprint.ItemType)
        {
            case ItemType.Heart:
                break;
            case ItemType.Coin:
                Main.Game.Player.GetCoin(blueprint.ItemAmount);
                break;
            case ItemType.Key:
                Main.Game.Player.GetKey(blueprint.ItemAmount);
                break;
            case ItemType.Bomb:
                Main.Game.Player.GetBomb(blueprint.ItemAmount);
                break;
        }

        Destroy(gameObject);
    }
}
