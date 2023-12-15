using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected JEH_Player _player;

    protected SpriteRenderer _spriteRenderer;
    protected Animator _animator;

    public float _moveSpeed;
    public int _atk;
    public float _range; // 캐릭터가 얼마나 떨어져있을때 공격할 것인지.

    protected Coroutine _stateCoroutine;


    protected enum State
    {
        Idle,
        Attack,
        Walk

    }
    protected State state;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, _range);
    }


    public virtual void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<JEH_Player>(); // 나중에 메인플레이어로 바꿀것
        _spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        _animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        state = State.Walk;
        // 초기화
    }


    protected virtual void FixedUpdate()
    {
        if (DistanceToTarget() < _range)
            Attack();
        else
            Move();
    }

    protected void OnDisable()
    {
        StopAllCoroutines();
        _stateCoroutine = null;
    }

    //---------------------------------------------------------------------------

    protected virtual void Move()
    {
        if (state == State.Walk) return;
        if (_stateCoroutine == null) return;

        state = State.Walk;

    }

    protected virtual IEnumerator MoveCoroutin()
    {
        while (true)
        {
            transform.Translate(DirectionToTarget() * _moveSpeed);
            yield return null;
        }
    }

    //---------------------------------------------------------------------------
    protected virtual void Attack()
    {
        if (state == State.Attack) return;
        if (_stateCoroutine == null) return;


        state = State.Attack;

    }

    protected virtual IEnumerator AttackCoroutin()
    {
        yield return null;
    }

    //---------------------------------------------------------------------------

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, _player.gameObject.transform.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (_player.gameObject.transform.position - transform.position).normalized;
    }


}