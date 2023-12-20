using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Properties

    public int CurrentHp { get; private set; }
    public int Coin { get; private set; }
    public int Key { get; private set; }
    public int Bomb { get; private set; }

    public StatUnit HP { get; private set; }
    public StatUnit Speed { get; private set; }
    public StatUnit Damage { get; private set; }
    public StatUnit AttackSpeed { get; private set; }
    public StatUnit AttackRange { get; private set; }
    public StatUnit ShotSpeed { get; private set; }

    public bool Invincible
    {
        get => _invincible;
        set
        {
            _invincible = value;
            if (_invincible)
            {
                if (_coInvincible != null) StopCoroutine(_coInvincible);
                _coInvincible = StartCoroutine(InvincibleTimer(_invincibilityTime));
            }
        }
    }
    #endregion

    #region Fields

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private bool _invincible = false;
    private Coroutine _coInvincible;
    [SerializeField] private float _invincibilityTime = 1f;
    private PlayerInputController _playerInputController;

    #endregion

    #region Init

    public Player()
    {
        HP = new StatUnit(6);
        Speed = new StatUnit(5f);
        Damage = new StatUnit(10);
        AttackSpeed = new StatUnit(0.3f);
        AttackRange = new StatUnit(2f);
        ShotSpeed = new StatUnit(5);

        CurrentHp = (int)HP.BaseValue;
        Coin = 10;
        Key = 0;
        Bomb = 1;
    }

    public void Initialize()
    {
        Main.UI.PlayerUI.Initialize(this);
    }

    private void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInputController = GetComponentInChildren<PlayerInputController>();
    }

    #endregion

    #region Inventory Method

    /// <summary>
    /// [정용태] 인벤토리 클래스
    /// </summary>

    public bool GetHeart(int amount)
    {
        if (CurrentHp + amount > HP.Value) return false;

        CurrentHp += amount;
        SFX.Instance.PlayOneShot(SFX.Instance.pickupHeart);
        Main.UI.PlayerUI.SetCurrentHP(CurrentHp.ToString());
        return true;
    }

    // 코인은 한도 초과를 넘겨도 계속 섭취 가능
    public bool GetCoin(int amount)
    {
        if (Coin + amount > Globals.MaxCoinStock) return true;
        Coin += amount;

        SFX.Instance.PlayOneShot(SFX.Instance.pickupCoin);
        Main.UI.PlayerUI.SetCoin(Coin.ToString());
        return true;
    }

    public bool GetKey(int amount)
    {
        if (Key + amount > Globals.MaxKeyStock) return false;
        Key += amount;

        SFX.Instance.PlayOneShot(SFX.Instance.pickupKey);
        Main.UI.PlayerUI.SetKey(Key.ToString());
        return true;
    }

    public bool GetBomb(int amount)
    {
        if (Bomb + amount > Globals.MaxBombStock) return false;
        Bomb += amount;

        SFX.Instance.PlayOneShot(SFX.Instance.pickupBomb);
        Main.UI.PlayerUI.SetBomb(Bomb.ToString());
        return true;
    }

    public bool UseCoin(int amount)
    {
        if (Coin - amount < 0) return false;

        Coin -= amount;
        Main.UI.PlayerUI.SetCoin(Coin.ToString());
        return true;
    }

    public bool UseKey()
    {
        if (Key - 1 < 0) return false;

        Key -= 1;
        SFX.Instance.PlayOneShot(SFX.Instance.useKey);
        Main.UI.PlayerUI.SetKey(Key.ToString());
        return true;
    }

    public bool UseBomb()
    {
        if (Bomb - 1 < 0) return false;

        Bomb -= 1;
        SFX.Instance.PlayOneShot(SFX.Instance.useBomb);
        Main.UI.PlayerUI.SetBomb(Bomb.ToString());
        return true;
    }

    #endregion

    #region Attribute Method

    public void Damaged(Transform target, int damage)
    {
        CurrentHp -= damage;
        SFX.Instance.PlayOneShot(SFX.Instance.playerHit);

        StartCoroutine(AlphaModifyAfterCollision());
        PlayerKnockback(target, damage);

        if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            Main.Game.GameStop();
        }

        Main.UI.PlayerUI.SetCurrentHP(CurrentHp.ToString());
    }


    private void PlayerKnockback(Transform target, int damage)
    {
        //target.transform.position - transform.position;
        Vector2 direction = -(target.transform.position - transform.position).normalized;
        Vector2 knockbackForce = direction * damage;

        _playerInputController.KnockbackDirection = knockbackForce;

        StartCoroutine(ResetKnockback());
    }

    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(0.1f);
        _playerInputController.KnockbackDirection = Vector2.zero;
    }

    #endregion

    #region OnCollision

    /// <summary>
    /// 문 접촉시 해당 방향에 맞는 다음방으로 이동
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("DoorCollider"))
        {
            if (collision.transform.name == "Up")
            {
                Main.Game.Dungeon.MoveToNextRoom(Vector2Int.up);
            }
            else if (collision.transform.name == "Down")
            {
                Main.Game.Dungeon.MoveToNextRoom(Vector2Int.down);
            }
            else if (collision.transform.name == "Left")
            {
                Main.Game.Dungeon.MoveToNextRoom(Vector2Int.left);
            }
            else if (collision.transform.name == "Right")
            {
                Main.Game.Dungeon.MoveToNextRoom(Vector2Int.right);
            }
        }
        if (Invincible && collision.gameObject.CompareTag("Enemy")) return;
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Damaged(collision.transform,1);
            Invincible = true;
        }
    }

    #endregion

    #region Coroutine

    private IEnumerator InvincibleTimer(float invincibilityTime)
    {
        yield return new WaitForSeconds(invincibilityTime);
        Invincible = false;
    }

    IEnumerator AlphaModifyAfterCollision()
    {
        float targetRed = 0.1f;

        Color startColor = _sprite.color;
        Color targetColor = new Color(startColor.a, targetRed, targetRed, targetRed);

        for (int i = 0; i < 3; ++i)
        {
            yield return FadeColor(startColor, targetColor, _invincibilityTime / 6);
            _sprite.color = targetColor;
            yield return FadeColor(targetColor, startColor, _invincibilityTime / 6);
            _sprite.color = startColor;
        }

        _sprite.color = startColor;
    }

    IEnumerator FadeColor(Color startColor, Color targetColor, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            _sprite.color = Color.Lerp(startColor, targetColor, Mathf.Clamp01(elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    #endregion
}
