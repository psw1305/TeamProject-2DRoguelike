using Unity.VisualScripting;
using UnityEngine;


public class RangeTrap : MonoBehaviour
{
    [SerializeField] private LayerMask activeTargetLayer;

    [SerializeField] private float trapShootDelay;

    [SerializeField] private GameObject bullet;
    [SerializeField] private int bulletDamage;
    [SerializeField] private int bulletRange;
    [SerializeField] private float bulletSpeed;

    private Vector3 _gameObjectPos;
    private Vector3 _targetPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("활성화");
            InvokeRepeating("ActiveRangeTrap", 0, trapShootDelay);
            InvokeRepeating("SeeTarget", 0, 0.1f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CancelInvoke();
        }
    }

    private void ActiveRangeTrap()
    {
        _gameObjectPos = transform.position;
        _targetPos = Main.Game.Player.transform.position;
        
        //Instantiate(bullet, transform.position, Quaternion.identity);
        EnemyProjectile trapProjectile = Main.Object.Spawn<EnemyProjectile>("TrapProjectile", gameObject.transform.position);
        trapProjectile.SetInfo(bulletDamage, bulletRange);
        trapProjectile.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(_gameObjectPos.x - _targetPos.x, _targetPos.y - _gameObjectPos.y));
        
        trapProjectile.SetVelocity((_targetPos - _gameObjectPos).normalized * bulletSpeed);
        trapProjectile.gameObject.tag = "EnemyProjectile";
    }

    private void SeeTarget()
    {
        _gameObjectPos = transform.position;
        _targetPos = Main.Game.Player.transform.position;
        transform.rotation = Quaternion.identity;
        transform.Rotate(0, 0, Mathf.Rad2Deg * Mathf.Atan2(_gameObjectPos.x - _targetPos.x, _targetPos.y - _gameObjectPos.y));
    }
}
