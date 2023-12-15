using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterRenderer;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    public void OnAim(Vector2 newAimDirection)
    {
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
}
