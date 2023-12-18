using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator _animator;
    private CircleCollider2D _collider;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _animator.SetTrigger("Explode");
        Invoke("ExplodeArea", 170/60f);
        Invoke("End", 210/60f);
    }

    private void ExplodeArea()
    {
        _collider.isTrigger = true;
        _collider.radius = 2;
    }

    private void End()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO => 적, 구조물, 플레이어에게 데미지
    }
}
