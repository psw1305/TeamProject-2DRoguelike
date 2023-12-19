using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    #region Properties
    public int Damage { get; private set; }
    public int AttackRange { get; private set; }
    #endregion

    #region Fields

    private Rigidbody2D _rigidbody;

    #endregion

    /// <summary>
    /// 벽에 닿으면 Projectile 제거
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Obstacle"))
        {
            if (this.IsValid()) Main.Object.Despawn(this);
        }

        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //몬스터 데미지 처리 TODO
            if (this.IsValid()) Main.Object.Despawn(this);
        }
    }

    public void Initialize()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
    }


    public void SetInfo(int  damage, int attackRange)
    {
        Initialize();
        Damage = damage;
        AttackRange = attackRange;
        if (this.gameObject.activeInHierarchy) StartCoroutine(CoCheckDestroy());

        
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rigidbody.velocity = velocity;
    }


    private IEnumerator CoCheckDestroy()
    {
        yield return new WaitForSeconds(AttackRange);

        Main.Object.Despawn(this);
    }
}
