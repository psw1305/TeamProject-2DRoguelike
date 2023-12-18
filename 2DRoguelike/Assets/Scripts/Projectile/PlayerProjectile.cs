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

    //private SpriteRenderer _sprite;
    private Rigidbody2D _rigidbody;

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag($"Wall"))
        {
            Destroy(this); //풀링 할 부분
        }
    }

    public void Initialize()
    {
    //    _sprite = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    public PlayerProjectile SetInfo(int  damage, int attackRange)
    {
        Initialize();
        Damage = damage;
        AttackRange = attackRange;

        if (this.gameObject.activeInHierarchy) StartCoroutine(CoCheckDestroy());

        return this;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rigidbody.velocity = velocity;
    }


    private IEnumerator CoCheckDestroy()
    {
        yield return new WaitForSeconds(AttackRange);
        
        Destroy(this); //풀링 할 부분
    }
}
