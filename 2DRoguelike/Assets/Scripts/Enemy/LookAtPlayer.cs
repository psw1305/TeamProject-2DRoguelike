using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    /// <summary>
    /// 플레이어 위치에따라 Sprite 반전시키는 스크립트
    /// </summary>


    private Player _target;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnEnable()
    {
        _target = Main.Game.Player;
    }

    private void Update()
    {

        if (_spriteRenderer != null)
        {
            _spriteRenderer.flipX = (transform.position.x > _target.transform.position.x);
        }

    }
}
