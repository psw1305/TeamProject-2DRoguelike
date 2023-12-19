using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerInputController : CharacterController
{
    #region Fileds

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform projectileSpawnPosition;


    // 테스트용 프리팹
    public GameObject bombPrefab;
    #endregion

    #region Properties

    private Camera _camera;
    private Vector2 _movementDirection = Vector2.zero;
    private Vector2 _aimDirection = Vector2.right;
    private Rigidbody2D _rigidbody;
    private Player _player;
    private float _timeSinceLastAttack = float.MaxValue;
    protected bool IsAttacking { get; set; }

    #endregion

    private void Awake()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        OnMoveEvent += Move;
        OnLookEvent += OnAim;
        OnAttackEvent += OnShoot;
    }

    protected override void Update()
    {
        base.Update();
        AttackDelay();
    }



    #region Move
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    private void FixedUpdate()
    {
        ApplyMovment(_movementDirection);
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void ApplyMovment(Vector2 direction)
    {
        direction = direction * 5;  //플레이어 speed

        _rigidbody.velocity = direction;
    }
    #endregion

    #region Look
    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        CallLookEvent(newAim);
    }

    public void OnAim(Vector2 newAimDirection)
    {
        _aimDirection = newAimDirection.normalized;
        RotatePlayer(newAimDirection);
    }

    private void RotatePlayer(Vector2 direction)
    {
        if (direction.x > 0)
        {
            // 오른쪽 방향일 때
            characterRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            // 왼쪽 방향일 때
            characterRenderer.flipX = true;
        }

        // 방향에 따라 스프라이트를 수직으로 뒤집거나 되돌립니다.
        if (direction.y > 0)
        {
            // 위쪽 방향일 때
            characterRenderer.flipY = false;
        }
        else if (direction.y < 0)
        {
            // 아래쪽 방향일 때
            characterRenderer.flipY = true;
        }
    }
    #endregion

    #region Attack
    public void OnFire(InputValue value)
    {
        Vector2 fireValue = value.Get<Vector2>();
        IsAttacking = fireValue.magnitude > 0f;
    }

    private void OnShoot(Vector2 direction)
    {
        CreateProjectile(direction);
    }

    private void CreateProjectile(Vector2 direction)
    {
        // 발사체의 Sprite 회전
        float angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // 발사체 생성
        PlayerProjectile projectile = Main.Object.Spawn<PlayerProjectile>("Projectile_Test", projectileSpawnPosition.position);
        projectile.SetInfo((int)Main.Game.Player.Damage.Value, (int)Main.Game.Player.AttackRange.Value);
        projectile.transform.rotation = rotation;
        projectile.SetVelocity(_aimDirection * (int)Main.Game.Player.ShotSpeed.Value);
        projectile.gameObject.tag = "PlayerProjectile";

    }

   

    private void AttackDelay()
    {
        if (_timeSinceLastAttack <= _player.AttackSpeed.Value)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        if (IsAttacking && _timeSinceLastAttack > _player.AttackSpeed.Value)
        {
            _timeSinceLastAttack = 0;
            CallAttackEvent(Vector2.zero);
        }
    }
    #endregion

    #region Bomb

    public void OnBoom()
    {
        CreateBomb();
    }

    private void CreateBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        
        StartCoroutine(Explosiontime(bomb, 3f)); //3초뒤 폭발
    }

    

    private IEnumerator Explosiontime(GameObject bomb, float delay)
    {
        yield return new WaitForSeconds(delay);
        // 폭팔에 대한 코드
        Destroy(bomb); //일단 지우기
    }

    #endregion
}
