using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private CharacterController _contoller;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 _aimDirection = Vector2.right;

    public GameObject testPrefab;

    private void Awake()
    {
        _contoller = GetComponent<CharacterController>();
    }

    void Start()
    {
        _contoller.OnAttackEvent += OnShoot;
        _contoller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        _aimDirection = newAimDirection.normalized; //방향 정규화
    }

    private void OnShoot(Vector2 direction)
    {
        CreateProjectile();
    }

    private void CreateProjectile()
    {
        // 발사 방향으로 회전된 각도 계산
        float angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // 발사체 생성
        Instantiate(testPrefab, projectileSpawnPosition.position, rotation);
    }
}
