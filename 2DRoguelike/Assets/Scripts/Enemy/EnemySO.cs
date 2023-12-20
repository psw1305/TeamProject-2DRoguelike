using UnityEngine;
[CreateAssetMenu(menuName = "EnemySO")]
public class EnemySO : ScriptableObject
{

  //  public int _id;
  //  public string _name;
    public int _maxHp;

  [HideInInspector]  public int _currentHp;

    public float _range; // 사정거리

    public float _movementSpeed;

    public float _attackSpeed; // 공격쿨타임
    public int _attackDamage;

    public float _bulletSpeed; // 총알 속도

    public int Phase = 0; // 기술순서
    public enum EnemyState
    {
        Ready, // 이벤트씬 용
        live,
        Die
    }
    [HideInInspector] public EnemyState enemyState;

}