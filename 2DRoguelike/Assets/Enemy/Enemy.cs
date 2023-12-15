using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected JEH_Player _player;

    protected SpriteRenderer _spriteRenderer;
    protected Animator _animator;

    protected float _speed;
    protected int _atk;

    protected Coroutine _stateCoroutine = null;

    public virtual void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<JEH_Player>(); // 나중에 메인플레이어로 바꿀것
        _spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        _animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {

    }



    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(_player._tag)) return;
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(_player._tag)) return;
    }

    //---------------------------------------------------------------------------

    protected void Move()
    {
        if (_stateCoroutine == null) return;

        StopCoroutine(_stateCoroutine);
        _stateCoroutine = StartCoroutine(MoveCoroutin());
    }
    protected virtual IEnumerator MoveCoroutin()
    {
        yield return null;
    }

    //---------------------------------------------------------------------------
    protected void Attack()
    {
        if (_stateCoroutine == null) return;

        StopCoroutine(_stateCoroutine);
        _stateCoroutine = StartCoroutine(AttackCoroutin());
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

    private void OnDisable()
    {
        StopAllCoroutines();
        _stateCoroutine = null;
    }

}