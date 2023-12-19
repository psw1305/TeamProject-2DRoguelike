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
    float startAngle;


    public Transform _target;

    public float attackAngle;



    public void UseSkills(EnemySkillType type)
    {
        switch (type)
        {
            case EnemySkillType.방사형:
                Circle();
                break;
            case EnemySkillType.플레이어방향:

                break;
        }

    }



    private void OnEnable()
    {


        //  StartCoroutine(AttackCoroutin());
    }

    protected Vector2 DirectionToTarget()
    {
        return (_target.gameObject.transform.position - transform.position).normalized;
    }
    protected float AngleToTarget()
    {
        return Mathf.Atan2(DirectionToTarget().y, DirectionToTarget().x) * Mathf.Rad2Deg;
    }



    void PlayerDirStraight() 
    {

        float minAngle = -(count / 2f) * 7 + 0.5f * 7; // z축을 7씩 벌린다

        for (int i = 0; i < count; i++)
        {
            float angle = minAngle + 7 * i;
            angle += AngleToTarget();
            BulletGenerator(angle);
        }


    }


    void Circle()
    {
        startAngle = AngleToTarget();



        float deltaAngle = 2 * Mathf.PI / count; // 360도를 Count 개수로 등분

        for (int i = 0; i < count; i++)
        {
            float currentAngle = i * deltaAngle + startAngle;
            float x = Mathf.Cos(currentAngle);
            float y = Mathf.Sin(currentAngle);
            Vector2 direction = new Vector2(x, y).normalized;

            float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            BulletGenerator(rot);

        }

    }


    void BulletGenerator(float rotate)
    {
        EnemyBullet obj = Instantiate(bullet);
        obj.gameObject.transform.SetParent(transform, false);
        obj.gameObject.transform.localRotation = Quaternion.Euler(0, 0, rotate);
        obj._bulletSpeed = 5;
    }


    IEnumerator AttackCoroutin()
    {
        while (true)
        {
            float deltaAngle = attackAngle / count;

            for (int i = 0; i < count; i++)
            {
                float currentAngle = i * deltaAngle + startAngle;
                float x = Mathf.Cos(currentAngle);
                float y = Mathf.Sin(currentAngle);
                Vector2 direction = new Vector2(x, y).normalized;

                float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Instantiate(bullet).gameObject.transform.localRotation = Quaternion.Euler(0, 0, rot);
            }

            yield return new WaitForSeconds(1f);
        }
    }


}

