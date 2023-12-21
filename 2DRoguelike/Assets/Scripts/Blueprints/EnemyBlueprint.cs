using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBlueprint", menuName = "Blueprint/Enemy")]
public class EnemyBlueprint : ScriptableObject
{
    [SerializeField] private int hp;
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int attackDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int phase = 0;     // 기술 순서
    [SerializeField] private EnemyBullet bullet;

    public int Hp => hp;
    public float Speed => speed;
    public float AttackRange => attackRange;
    public float AttackSpeed => attackSpeed;
    public int AttackDamage => attackDamage;
    public float BulletSpeed => bulletSpeed;
    public int Phase => phase;
    public EnemyBullet Bullet => bullet;
}