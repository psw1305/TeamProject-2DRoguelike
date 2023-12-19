using UnityEngine;

public class Player : MonoBehaviour
{
    #region Properties

    public StatUnit HP { get; private set; }
    public StatUnit Damage { get; private set; }
    public StatUnit Speed { get; private set; }
    public StatUnit AttackRange { get; private set; }
    public StatUnit AttackSpeed { get; private set; }
    public StatUnit ShotSpeed { get; private set; }
    public float CurrentHp { get; private set; }

    #endregion

    #region Init

    public Player()
    {
        HP = new StatUnit(3);
        Damage = new StatUnit(10);
        Speed = new StatUnit(2);
        AttackRange = new StatUnit(10);
        AttackSpeed = new StatUnit(2);
        ShotSpeed = new StatUnit(2);

        CurrentHp = HP.BaseValue;
    }

    #endregion

    #region Method

    public void Damaged(int damage)
    {
        CurrentHp -= damage;
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
