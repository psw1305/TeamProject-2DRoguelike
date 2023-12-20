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
            Destroy(other);
        }

        if (other.CompareTag("PickupItem") || other.CompareTag("Bomb") || other.CompareTag("Enemy"))
        {
            Vector2 direction = other.transform.position - transform.position;
            Vector2 knockbackForce = direction.normalized * explosionForce;

            if (other.TryGetComponent<Rigidbody2D>(out var targetRigidbody))
            {
                targetRigidbody.AddForce(knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
