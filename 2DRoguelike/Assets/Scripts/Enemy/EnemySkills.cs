using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkills : MonoBehaviour
{
    public enum EnemySkillType
    {
        방사형,
        플레이어방향
    }

    public EnemySkillType enemySkillType;

    public EnemyBullet bullet;
    public int count;
    public float StartAngle;


    void OnEnable()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }


    public void UseSkills(EnemySkillType type)
    {
        switch (type)
        {
            case EnemySkillType.방사형:
                StartCoroutine(Circle());
                break;
            case EnemySkillType.플레이어방향:
                break;
        }

    }



    IEnumerator Circle()
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



    public void SkillStop()
    {
        StopAllCoroutines();
    }


    private void OnDisable()
    {
        SkillStop();
    }

}


/*

public int Count;
public float attackAngle;


void OnEnable()
{
    _target = Main.Game.Player;
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

 */