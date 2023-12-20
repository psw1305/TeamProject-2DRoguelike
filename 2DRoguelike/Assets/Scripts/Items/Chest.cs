using System.Collections;
using UnityEngine;

public class Chest : PickupItem
{
    [Header("Field")]
    [SerializeField] private int amount;
    [SerializeField] private bool isKey;
    private bool _isOpen = false;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite chestCloseSprite;

    protected override void PlayerItemPickup()
    {
        if (!_isOpen)
        {
            StartCoroutine(OpenChest());
        }
    }

    private IEnumerator OpenChest()
    {
        if (isKey && !Main.Game.Player.UseKey()) yield break;

        _isOpen = true;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = chestCloseSprite;

        for (int i = 0; i < amount; i++)
        {
            Main.Reward.PickupItemDrop(this.transform.position, Main.Game.Dungeon.CurrentRoom.ObjectContainer);
        }
    }
}
