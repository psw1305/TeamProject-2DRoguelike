using UnityEngine;

public class SpikeTrap : Obstacle
{
    [SerializeField] private LayerMask activeTargetLayer;

    [SerializeField] private int trapDamage;
    [SerializeField] private float trapActiveDelay;
    [SerializeField] private float damageDelay;

    [SerializeField] private RuntimeAnimatorController animController;
    private Animator _animator;

    private void Start()
    {
        gameObject.AddComponent<Animator>();
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = animController;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("ActiveTrap", trapActiveDelay);
        }
        base.OnTriggerEnter2D(collision);
    }

    private void ActiveTrap()
    {
        _animator.SetBool("isActive", true);
        InvokeRepeating("GiveDamage", 0, damageDelay);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetBool("isActive", false);
            CancelInvoke();
        }
    }

    private void GiveDamage()
    {
        if (_animator.GetBool("isActive"))
        {
            Main.Game.Player.Damaged(transform, trapDamage);
        }
    }
}
