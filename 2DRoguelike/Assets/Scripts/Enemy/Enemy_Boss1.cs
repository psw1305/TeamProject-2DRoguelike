using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Boss1 : Enemy
{

    public GameObject hpSlider;
    public Animator introAnim;


    private Animator _intro;
    private Slider _hpSlider;


    protected override void OnEnable()
    {
        base.OnEnable();
        Initialize();
    }

    void Update()
    {

        if (_enemyState != EnemySO.EnemyState.live) return;

        Move();
    }


    void HpbarDestroy()
    {
        Destroy(_hpSlider.gameObject);
    }

    void HpbarChange()
    {
        _hpSlider.value = _currentHp;
    }


    void Initialize()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _movementSpeed;
        _agent.stoppingDistance = _range;

        dieAction -= HpbarDestroy;
        dieAction += HpbarDestroy;
        damagedAction -= HpbarChange;
        damagedAction += HpbarChange;

        _hpSlider = Instantiate(hpSlider).GetComponentInChildren<Slider>();

        _hpSlider.maxValue = _maxHp;
        _hpSlider.value = _maxHp;
        _hpSlider.minValue = 0;

        _enemyState = EnemySO.EnemyState.Ready;

        _intro = Instantiate(introAnim);

        BGM.Instance.Stop();
        SFX.Instance.PlayOneShot(SFX.Instance.bossIntro);

        Invoke("BattleBegen", 2.467f);

    }


    void BattleBegen()
    {
        Destroy(_intro.gameObject);
        _enemyState = EnemySO.EnemyState.live;

        BGM.Instance.Play(BGM.Instance.boss, true);
    }


    void Move()
    {
        if (_target == null) return;

        _agent.SetDestination(_target.transform.position);


        if (_agent.velocity.magnitude > 0.2f) // 움직이는 중이면 true
        {
            _animator?.SetBool(isWalkHash, true);
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
            _agent.stoppingDistance = Mathf.Clamp(_agent.stoppingDistance - 0.1f, 1.2f, _range);
            return;
        }

        _agent.stoppingDistance = _range; // 시야거리 초기화
        _attackCoroutine = StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (!IsTargetStraight())
                StopStateCoroutin();


            if (_maxHp * 0.10 >= _currentHp)
            {
                Circle(8, 6);

                yield return new WaitForSeconds(1f);

                FanShape(5, 14, 5);

                yield return new WaitForSeconds(1f);
            }
            else if (_maxHp * 0.30 >= _currentHp)
            {
                Circle(6, 3);

                yield return new WaitForSeconds(1.4f);

            }
            else if (_maxHp * 0.50 >= _currentHp)
            {
                FanShape(1, 7, 7);

                yield return new WaitForSeconds(0.5f);

                FanShape(9, 7, 5, true);

                yield return new WaitForSeconds(1);

            }
            else if (_maxHp * 0.75 >= _currentHp)
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
