using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
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
        string targetLayerName = "Obstacle";

        if (collision.gameObject.CompareTag("Wall") || IsLayer(collision.gameObject.layer, targetLayerName))
        {
            if (this.IsValid()) Main.Object.Despawn(this);
        }

        if (Main.Game.Player.Invincible && collision.gameObject.CompareTag("Player")) return;
        else if (collision.gameObject.CompareTag("Player"))
        {
            Main.Game.Player.Damaged(transform, 1);
            Main.Game.Player.Invincible = true;
            if (this.IsValid()) Main.Object.Despawn(this);
        }
    }
    private bool IsLayer(int layer, string layerName)
    {
        int targetLayer = LayerMask.NameToLayer(layerName);
        return layer == targetLayer;
    }

    public void Initialize()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
    }


    public void SetInfo(int damage, int attackRange)
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
