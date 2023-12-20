using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomBlueprint", menuName = "Blueprint/Room")]
public class RoomBlueprint : ScriptableObject
{
    [SerializeField] private Sprite floor;
    [SerializeField] private bool isReward;

    [Header("Object Position")]
    [SerializeField] private Vector2 rewardPosition;
    [SerializeField] private List<PairWithObjectVector2> obstacleList = new();
    [SerializeField] private List<PairWithObjectVector2> itemList = new();
    [SerializeField] private List<PairWithObjectVector2> enemyList = new();

    public Sprite Floor => floor;
    public bool IsReward => isReward;
    public Vector2 RewardPosition => rewardPosition;
    public List<PairWithObjectVector2> ObstacleList => obstacleList;
    public List<PairWithObjectVector2> ItemList => itemList;
    public List<PairWithObjectVector2> EnemyList => enemyList;
}
