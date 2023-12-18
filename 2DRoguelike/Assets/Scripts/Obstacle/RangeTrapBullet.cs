using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RangeTrapBullet : MonoBehaviour
{
    [SerializeField] float speed;

    private Vector3 _gameObjectPos;
    private Vector3 _targetPos;
    private void Start()
    {
        _gameObjectPos = transform.position;
        _targetPos = Main.Game.Player.transform.position;
        transform.Rotate(0, 0, Mathf.Rad2Deg * Mathf.Atan2(_gameObjectPos.x - _targetPos.x, _gameObjectPos.y - _targetPos.y));
        GetComponent<Rigidbody2D>().velocity = (_targetPos - _gameObjectPos).normalized * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Main.Game.Player.Damaged(1);
            Destroy(gameObject);
        }
    }
}
