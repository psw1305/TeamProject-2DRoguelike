using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Boss1 : Enemy
{
    public GameObject hpSlider;
    public Animator introAnim;
    public GameObject dieEffect;

    private Animator _intro;
    private Slider _hpSlider;

    protected override void OnEnable()
    {
        base.OnEnable();
        Initialize();
    }

    void Update()
    {
        if (_enemyState != EnemyState.live) return;

        Move();
    }

    void HpbarDestroy()
    {
        Destroy(_hpSlider.gameObject);
    }

    void HpbarChange()
    {
        _hpSlider.value = CurrentHp;
    }

    protected void DieEffectGenerator()
    {
        GameObject obj = Instantiate(dieEffect);
        obj.gameObject.transform.SetParent(transform, false);
        obj.transform.SetParent(Root);
    }

    void Initialize()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = Speed;
        _agent.stoppingDistance = AttackRange;

        dieAction -= HpbarDestroy;
        dieAction += HpbarDestroy;
        damagedAction -= HpbarChange;
        damagedAction += HpbarChange;
        dieAction -= DieEffectGenerator;
        dieAction += DieEffectGenerator;

        _hpSlider = Instantiate(hpSlider).GetComponentInChildren<Slider>();

        _hpSlider.maxValue = MaxHp;
        _hpSlider.value = MaxHp;
        _hpSlider.minValue = 0;

        _enemyState = EnemyState.Ready;

        _intro = Instantiate(introAnim);

        BGM.Instance.Stop();
        SFX.Instance.PlayOneShot(SFX.Instance.bossIntro);

        Invoke("BattleBegen", 2.467f);
    }


    void BattleBegen()
    {
        Destroy(_intro.gameObject);
        _enemyState = EnemyState.live;
        BGM.Instance.Play(BGM.Instance.boss, true);
    }


    void Move()
    {
        if (_target == null) return;

        _agent.SetDestination(_target.transform.position);

        if (_agent.velocity.magnitude > 0.2f) // 움직이는 중이면 true
        {
            _animator.SetBool(IsWalkHash, true);
            StopStateCoroutin();
        }
        else
        {
            TakeAim();
        }
    }


    protected void TakeAim()
    {
        if (_attackCoroutine != null)
            return;

        if (!IsTargetStraight())
        {
            _agent.stoppingDistance = Mathf.Clamp(_agent.stoppingDistance - 0.1f, 1.2f, AttackRange);
            return;
        }

        _agent.stoppingDistance = AttackRange;   // 시야거리 초기화
        _attackCoroutine = StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (!IsTargetStraight()) StopStateCoroutin();

            if (MaxHp * 0.10 >= CurrentHp)
            {
                Circle(8, 6);
                yield return new WaitForSeconds(1f);

                FanShape(5, 14, 5);
                yield return new WaitForSeconds(1f);
            }
            else if (MaxHp * 0.30 >= CurrentHp)
            {
                Circle(6, 3);
                yield return new WaitForSeconds(1.4f);

            }
            else if (MaxHp * 0.50 >= CurrentHp)
            {
                FanShape(1, 7, 7);
                yield return new WaitForSeconds(0.5f);

                FanShape(9, 7, 5, true);
                yield return new WaitForSeconds(1);
            }
            else if (MaxHp * 0.75 >= CurrentHp)
            {
                FanShape(7, 40);
                yield return new WaitForSeconds(1);
            }
            else
            {
                FanShape(3, 20);
                yield return new WaitForSeconds(1.3f);

                FanShape();
                yield return new WaitForSeconds(0.3f);

                FanShape();
                yield return new WaitForSeconds(0.3f);

                FanShape();
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
