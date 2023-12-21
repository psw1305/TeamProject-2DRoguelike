using UnityEngine;
using TMPro;

public class ShopItem : InteractableItem
{
    [SerializeField] private TextMeshProUGUI costText;

    private void Start()
    {
        costText.text = $"{targetBp.itemCost}$";
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (!Main.Game.Player.UseCoin(targetBp.itemCost)) return;

        SFX.Instance.PlayOneShot(SFX.Instance.getPassiveItem);
        _equipSystem.GetItemStatus(targetBp);
        Destroy(gameObject);
    }
}
