using System.Collections;
using UnityEngine;

public class Enemy_Ranged : Enemy
{
    public GameObject bulletPrefab;

    protected override void OnEnable()
    {
        _speed = 0.1f;
        _atk = 1;

        _stateCoroutine = StartCoroutine(AttackCoroutin());
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
     //   base.OnTriggerEnter2D(collision);
     //   Attack();
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
     //   base.OnTriggerExit2D(collision);
     //   Move();

    }



    protected override IEnumerator MoveCoroutin()
    {
        yield return StartCoroutine(base.MoveCoroutin());

        while (true)
        {
            transform.Translate(DirectionToTarget() * _speed);
            yield return null;
        }
    }

    protected override IEnumerator AttackCoroutin()
    {
        yield return StartCoroutine(base.AttackCoroutin());

        while (true)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }
}