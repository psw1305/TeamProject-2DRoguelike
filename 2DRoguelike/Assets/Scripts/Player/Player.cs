using UnityEngine;

public class Player : MonoBehaviour
{
    #region Properties

    public int HP { get; private set; }
    public int Damage { get; private set; }
    public int Speed { get; private set; }
    public int AttackRange { get; private set; }
    public int AttackSpeed { get; private set; }
    public int ShotSpeed { get; private set; }

    #endregion

    #region Init

    public Player()
    {
        HP = 3;
        Damage = 10;
        Speed = 2;
        AttackRange = 10;
        AttackSpeed = 2;
        ShotSpeed = 2;
    }

    #endregion

    #region Method

    public void Damaged(int damage)
    {
        HP -= damage;
    }

    #endregion

    #region OnCollision

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
}
