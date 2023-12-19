using TMPro;
using UnityEngine;

public class UI_Player : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private TextMeshProUGUI bombText;

    [Header("Attribute")]
    [SerializeField] private TextMeshProUGUI statSpeedText;
    [SerializeField] private TextMeshProUGUI statDamageText;
    [SerializeField] private TextMeshProUGUI statAttackSpeedText;
    [SerializeField] private TextMeshProUGUI statAttackRangeText;
    [SerializeField] private TextMeshProUGUI statShotSpeedText;

    #region Init

    public void Initialize(Player player)
    {
        SetInventory(player);
        SetAttribute(player);
    }

    public void SetInventory(Player player)
    {
        hpText.text = player.CurrentHp.ToString();
        coinText.text = player.Coin.ToString();
        keyText.text = player.Key.ToString();
        bombText.text = player.Bomb.ToString();
    }

    public void SetAttribute(Player player)
    {
        statSpeedText.text = player.Speed.Value.ToString();
        statDamageText.text = player.Damage.Value.ToString();
        statAttackSpeedText.text = player.AttackSpeed.Value.ToString();
        statAttackRangeText.text = player.AttackRange.Value.ToString();
        statShotSpeedText.text = player.ShotSpeed.Value.ToString();
    }

    public void SetCurrentHP(string hpText)
    {
        this.hpText.text = hpText;
    }

    public void SetCoin(string coinText)
    {
        this.coinText.text = coinText;
    }

    public void SetKey(string keyText)
    {
        this.keyText.text = keyText;
    }

    public void SetBomb(string bombText)
    {
        this.bombText.text = bombText;
    }

    #endregion

}
