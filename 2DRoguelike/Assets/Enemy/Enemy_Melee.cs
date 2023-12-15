using System.Collections;
using UnityEngine;

public class Enemy_Melee : Enemy
{


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
            _player.Damaged(_atk);
            Debug.Log("몸통박치기!");
        }
    }


}
