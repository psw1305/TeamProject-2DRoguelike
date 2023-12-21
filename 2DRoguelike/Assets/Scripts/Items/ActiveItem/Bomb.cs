using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float explosionForce;
    [SerializeField] private Animator _animator;
    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        _animator.SetTrigger("Explode");

        yield return new WaitForSeconds(2f);

        SFX.Instance.PlayOneShot(SFX.Instance.explodeBomb);
        _collider.isTrigger = true;
        _collider.radius = 1.5f;

        yield return new WaitForSeconds(0.25f); ;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_collider.isTrigger) return;

        if (other.CompareTag("Obstacle"))
        {
            if (other.TryGetComponent<Obstacle>(out var obstacle))
            {
                obstacle.DestroySelf();
            }
        }

        if (other.CompareTag("PickupItem"))
        {
            TargetKnockback(other.transform);
        }

        if (other.CompareTag("Enemy"))
        {
            TargetKnockback(other.transform);

            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.Damaged(30);
            }
        }

        if (other.CompareTag("Player"))
        {
            //TargetKnockback(other.transform);

            if (other.TryGetComponent<Player>(out var player))
            {
                player.Damaged(transform, 2);
            }
        }
    }

    private void TargetKnockback(Transform target)
    {
        Vector2 direction = target.transform.position - transform.position;
        Vector2 knockbackForce = direction.normalized * explosionForce;

        if (target.TryGetComponent<Rigidbody2D>(out var targetRigidbody))
        {
            targetRigidbody.AddForce(knockbackForce, ForceMode2D.Impulse);
        }
    }
}
