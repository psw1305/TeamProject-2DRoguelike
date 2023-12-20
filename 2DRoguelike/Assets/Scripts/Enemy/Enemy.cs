using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    #region Fileds

    static Transform root;
    static Transform Root
    {
        get
        {
            if (root == null)
            {
                GameObject obj = new() { name = $"[Pool_Root] Enemy Bullet" };
                root = obj.transform;
            }
            return root;
        }
    }

    public EnemyBullet bullet;
    

    protected int _id;
    protected string _name;
    protected int _maxHp;
    protected int _currentHp;
    protected float _range; // 사정거리

    protected float _movementSpeed;

    protected float _attackSpeed; // 공격쿨타임
    protected int _attackDamage;

    protected float _bulletSpeed; // 총알 속도

    protected int Phase = 0; // 기술순서


    protected Player _target;
    protected NavMeshAgent _agent;
    protected Coroutine _attackCoroutine;
    protected RaycastHit2D _rayHit;

    protected SpriteRenderer _spriteRenderer;
    protected Animator _animator;

    protected enum EnemyState
    {
        Ready, // 이벤트씬 용
        live,
        Die
    }
    protected EnemyState enemyState;


    protected readonly int isWalkHash = Animator.StringToHash("isWalk");
    protected readonly int AttackHash = Animator.StringToHash("Attack");
    protected readonly int DieHash = Animator.StringToHash("Die");


    #endregion


    protected virtual void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _attackCoroutine = null;

    }

    protected virtual void OnEnable()
    {
        _target = Main.Game.Player;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        enemyState = EnemyState.Ready;
        enemyState = EnemyState.live;
    }


    private void OnDisable()
    {
        if (_attackCoroutine != null) StopCoroutine(_attackCoroutine);

        _attackCoroutine = null;
    }

    protected void StopStateCoroutin()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }

    #region Damaged

    public void Damaged(int damage)
    {
        if (enemyState != EnemyState.live) return;

        _currentHp -= damage;

        StartCoroutine(FlickerCharacter());

        if (_currentHp <= 0)
        {
            enemyState = EnemyState.Die;
            StopAllCoroutines();
            Main.Game.Dungeon.CurrentRoom.CheckRoomClear();     // 적 사망 => 방 클리어 조건 체크

            _animator.SetTrigger(DieHash);
            // 사라지는 이펙트 추가
            Destroy(gameObject);
            
        }
    }

    public virtual IEnumerator FlickerCharacter()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
    }

    #endregion

    #region Skill


    protected void FanShape(int bulletCount = 1, float rot = 7f, float bulletSpeed = 5f, bool isRandom = false) // 플레이어방향 부채꼴 공격
    {
        float minAngle = -(bulletCount / 2f) * rot + 0.5f * rot;  // 탄환끼리 rot 각도로 벌린다

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = minAngle + rot * i;
            angle += AngleToTarget();
            BulletGenerator(angle, bulletSpeed);
        }
    }

    protected void Circle(int bulletCount = 1, float bulletSpeed = 5f, bool isRandom = false) // 방사형 공격
    {

        float deltaAngle = 2 * Mathf.PI / bulletCount; // 360도를 Count 개수로 등분

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = i * deltaAngle;
            currentAngle += AngleToTarget() / 2f;
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
        obj.transform.SetParent(Root);
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

        if (_rayHit && (_rayHit.collider.gameObject.CompareTag("Enemy") || _rayHit.collider.gameObject.CompareTag("EnemyBullet")))
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