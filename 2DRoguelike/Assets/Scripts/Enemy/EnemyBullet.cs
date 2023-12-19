using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
   public float _bulletSpeed = 5f;
   public int _damage = 1;

    private void OnEnable()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        transform.Translate(Vector3.right * _bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            Main.Game.Player.Damaged(_damage);
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("DoorCollider"))
        {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Untagged") || collision.gameObject.CompareTag("Obstacle")) 
            return;

        Destroy(gameObject);

    }
}
