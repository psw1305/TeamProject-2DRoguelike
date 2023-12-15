using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : CharacterController
{
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        CallLookEvent(newAim);
    }

    public void OnFire(InputValue value)
    {
        Vector2 fireValue = value.Get<Vector2>();
        CallAttackEvent(fireValue);
    }

    public void OnBoom(InputValue value)
    {
        Debug.Log("OnBoom" + value.ToString());

    }
}
