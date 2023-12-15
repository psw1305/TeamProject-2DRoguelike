using System.Collections;
using UnityEngine;

public class Enemy_Ranged : Enemy
{
    public Enemy_Bullet bulletPrefab;

    protected override void OnEnable()
    {

    }


    protected override void Attack()
    {
        if (_stateCoroutine != null)
            StopCoroutine(_stateCoroutine);

        _stateCoroutine = StartCoroutine(AttackCoroutin());
    }
    protected override void Move()
    {
        if (_stateCoroutine != null)
            StopCoroutine(_stateCoroutine);

        _stateCoroutine = StartCoroutine(MoveCoroutin());
    }



    protected override IEnumerator AttackCoroutin()
    {
        yield return StartCoroutine(base.AttackCoroutin());

        while (true)
        {
            yield return new WaitForSeconds(1f);
            Enemy_Bullet obj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            obj.dir = DirectionToTarget();
            obj.speed = 1f;
            obj.atk = 1;
        }
    }
}