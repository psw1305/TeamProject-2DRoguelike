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

    #endregion

    #region Init

    public Player()
    {
        HP = new StatUnit(3);
        Speed = new StatUnit(2);
        Damage = new StatUnit(10);
        AttackSpeed = new StatUnit(2);
        AttackRange = new StatUnit(10);
        ShotSpeed = new StatUnit(2);

        CurrentHp = (int)HP.BaseValue;
        Coin = 10;
        Key = 10;
        Bomb = 10;
    }

    public void Initialize()
    {
        Main.UI.PlayerUI.Initialize(this);
    }

    #endregion

    #region Inventory Method

    /// <summary>
    /// [정용태] 인벤토리 클래스
    /// </summary>

    public bool GetCoin(int amount)
    {
        if (Coin + amount > Globals.MaxCoinStock) return false;
        Coin += amount;
        Main.UI.PlayerUI.SetCoin(Coin.ToString());
        return true;
    }

    public bool GetKey(int amount)
    {
        if (Key + amount > Globals.MaxKeyStock) return false;
        Key += amount;
        Main.UI.PlayerUI.SetKey(Key.ToString());
        return true;
    }

    public bool GetBomb(int amount)
    {
        if (Bomb + amount > Globals.MaxBombStock) return false;
        Bomb += amount;
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
        Main.UI.PlayerUI.SetKey(Key.ToString());
        return true;
    }

    public bool UseBomb()
    {
        if (Bomb - 1 < 0) return false;

        Bomb -= 1;
        Main.UI.PlayerUI.SetBomb(Bomb.ToString());
        return true;
    }

    #endregion

    #region Attribute Method

    public void Damaged(int damage)
    {
        CurrentHp -= damage;
        Main.UI.PlayerUI.SetCurrentHP(CurrentHp.ToString());
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
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile") || collision.CompareTag("Enemy"))
        {
            if (collision.CompareTag("EnemyProjectile"))
            {
                Projectile projectile = collision.gameObject.GetComponent<Projectile>();
                Main.Resource.Destroy(collision.gameObject);
                Damaged(projectile.Damage);
                //무적시간 만들어야됨
            }
            else
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                Damaged(1); //임의로 1로 지정 Enemy의 Demage가 있어야함
                //무적시간
            }

            //플레이어 하트 UI에 대한 작업
        }
    }

    
}
