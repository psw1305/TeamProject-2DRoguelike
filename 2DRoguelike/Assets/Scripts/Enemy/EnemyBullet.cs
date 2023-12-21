using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float _bulletSpeed = 5f;
    private int _damage = 1;

    public void SetBulletSpeed(float bulletSpeed)
    {
        _bulletSpeed = bulletSpeed;
    }

    private void OnEnable()
    {
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        transform.Translate(_bulletSpeed * Time.deltaTime * Vector3.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Main.Game.Player.Invincible && collision.gameObject.CompareTag("Player")) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            Main.Game.Player.Damaged(transform, _damage);
            Main.Game.Player.Invincible = true;
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("DoorCollider"))
        {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Untagged") || collision.gameObject.CompareTag("Obstacle")|| collision.gameObject.CompareTag("PlayerProjectile")) 
            return;

        Destroy(gameObject);

    }
}
