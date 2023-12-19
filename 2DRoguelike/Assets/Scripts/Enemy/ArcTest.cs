using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ArcTest : MonoBehaviour
{

    private JEH_Player _target;


    public EnemyBullet bullet;
    public int Count;
    public float attackAngle;

    /// <summary>
    /// 
    /// 플레이어위치 float 각도로 가져오기
    ///      Vector2 v2 = _target.transform.position - transform.position;
    ///          StartAngle = Mathf.Atan2(v2.y, v2.x)* Mathf.Rad2Deg;
    /// 
    /// 
    /// 부채꼴 공격
    /// 
    /// 총알간 간격 = 부채꼴 각도 범위 / 총알개수
    ///     총알개수 만큼 반복
    ///   각도 = 총알간 간격* i + 총알간 간격 /2 - 부채꼴 범위 /2
    /// 
    /// </summary>
    void OnEnable()
    {
        _target = FindObjectOfType<JEH_Player>();
        StartCoroutine(AttackCoroutin());

    }

    IEnumerator AttackCoroutin()
    {
        while (true)
        {
          
            var rad = attackAngle * Mathf.Deg2Rad;


            float deltaAngle = rad / Count;

            for (int i = 0; i < Count; i++)
            {
                Vector2 direction = new Vector2(Mathf.Cos(deltaAngle * i), Mathf.Sin(deltaAngle * i)).normalized;

                Instantiate(bullet);

            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}

/*

bullet.transform.position = transform.position;
bullet.transform.rotation = Quaternion.identity;

Rigidbody2D rigidR = bullet.GetComponent<Rigidbody2D>();
Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 3 * curPatternCount / maxPatternCount[patternIndex]), -1);
rigidR.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);


*/

/*
 * 
 * 뭉텅이로 날아감
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcTest : MonoBehaviour
{

    private JEH_Player _target;


    public EnemyBullet bullet;
    public int Count = 40;
    public float StartAngle;

    /// <summary>
    /// 
    /// count 갯수만큼 사방으로 퍼져서 공격
    /// </summary>
    void OnEnable()
    {

        _target = FindObjectOfType<JEH_Player>();
        StartCoroutine(AttackCoroutin());
    }

    IEnumerator AttackCoroutin()
    {
        while (true)
        {
            float deltaAngle = 2 * 90f / Count;


            for (int i = 0; i < Count; i++)
            {
                float currentAngle = i * deltaAngle + StartAngle;
                float x = Mathf.Cos(currentAngle);
                float y = Mathf.Sin(currentAngle);
                Vector2 direction = new Vector2(x, y).normalized;


                Instantiate(bullet)._target = direction;

            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

}


*/