using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RingTest : MonoBehaviour
{
    private JEH_Player _target;


    public EnemyBullet bullet;
    public int count;
    public float StartAngle;


    /// <summary>
    /// 
    /// count 갯수만큼 사방으로 퍼져서 공격
    /// </summary>
    /// 

    void OnEnable()
    {

        _target = FindObjectOfType<JEH_Player>();


     //   Vector2 v2 = _target.transform.position - transform.position;
     //   StartAngle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;


        StartCoroutine(AttackCoroutin());



    }

    IEnumerator AttackCoroutin()
    {
        while (true)
        {
            float deltaAngle = 2 * Mathf.PI / count; // 360도를 Count 개수로 등분

            for (int i = 0; i < count; i++)
            {
                float currentAngle = i * deltaAngle + StartAngle;
                float x = Mathf.Cos(currentAngle);
                float y = Mathf.Sin(currentAngle);
                Vector2 direction = new Vector2(x, y).normalized;

                float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Instantiate(bullet).gameObject.transform.localRotation = Quaternion.Euler(0, 0, rot);
            }
            yield return new WaitForSeconds(1f);
        }
    }



    private void OnDisable()
    {
        StopAllCoroutines();
    }

}
