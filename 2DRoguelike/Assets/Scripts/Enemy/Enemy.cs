using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    protected int _id;
    protected string _name;
    protected int _maxHp;
    protected int _currentHp;
    protected float _range; // 사정거리

    protected float _movementSpeed;
    
    protected float _attackSpeed; // 공격쿨타임
    protected int _attackDamage;

    protected float _bulletSpeed; // 총알 속도

    protected Player _target;
    protected NavMeshAgent _agent;
    protected Coroutine _stateCoroutine;
    protected RaycastHit2D _rayHit;


    protected enum EnemyState
    {
        live,
        Die
    }
    protected EnemyState enemyState;


    public EnemyBullet bullet;


    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _stateCoroutine = null;
    
    }

    protected virtual void OnEnable()
    {
        _target = Main.Game.Player;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        enemyState = EnemyState.live; 
    }


    private void OnDisable()
    {
        StopCoroutine(_stateCoroutine);
        _stateCoroutine = null;
    }


    public void Damaged(int damage)
    {
        if (enemyState == EnemyState.Die) return;

        _currentHp -= damage;

        if (_currentHp <= 0)
        {
            enemyState = EnemyState.Die;
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
    protected void StopStateCoroutin()
    {
        if (_stateCoroutine != null)
        {
            StopCoroutine(_stateCoroutine);
            _stateCoroutine = null;
        }
    }


    #region Skill

    protected void FanShape(int bulletCount, float bulletSpeed, bool isRandom) // 플레이어방향 공격
    {
        float minAngle = -(bulletCount / 2f) * 7 + 0.5f * 7; // z축을 7씩 벌린다

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = minAngle + 7 * i;
            angle += AngleToTarget();
            BulletGenerator(angle, bulletSpeed);
        }

    }


    protected void Circle(int bulletCount, float bulletSpeed ,bool isRandom)
    {

        float deltaAngle = 2 * Mathf.PI / bulletCount; // 360도를 Count 개수로 등분

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = i * deltaAngle + AngleToTarget();
            float x = Mathf.Cos(currentAngle);
            float y = Mathf.Sin(currentAngle);
            Vector2 direction = new Vector2(x, y).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            BulletGenerator(angle, bulletSpeed);

        }

    }

   protected void BulletGenerator(float rotate, float speed)
    {
        EnemyBullet obj = Instantiate(bullet);
        obj.gameObject.transform.SetParent(transform, false);
        obj.gameObject.transform.localRotation = Quaternion.Euler(0, 0, rotate);

        obj._bulletSpeed = speed;
    }

    #endregion

    #region Util


    protected bool IsTargetStraight() // 벽에 가려지지않은 플레이어를 보고있는지 체크
    {
        _rayHit = Physics2D.Raycast(transform.position, DirectionToTarget());

        if (_rayHit && _rayHit.collider.gameObject.CompareTag("Player"))
        {
            return true;
        }

        if (_rayHit.collider.gameObject.CompareTag("Enemy") || _rayHit.collider.gameObject.CompareTag("EnemyBullet"))
        {
            return true;
        }

        return false;
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, _target.gameObject.transform.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (_target.gameObject.transform.position - transform.position).normalized;
    }
    protected float AngleToTarget()
    {
        return Mathf.Atan2(DirectionToTarget().y, DirectionToTarget().x) * Mathf.Rad2Deg;
    }


    void OnDrawGizmosSelected() // 사정거리보기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }


    #endregion

}