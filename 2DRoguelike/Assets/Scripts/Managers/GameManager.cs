using UnityEngine;

public class GameManager
{
    #region Properties

    public Dungeon Dungeon { get; private set; }
    public Player Player { get; private set; }

    #endregion

    #region Init

    public void Initialize()
    {
        var dungeonPrefab = GameObject.Instantiate(Main.Resource.GetObject("Dungeon"));
        Dungeon = dungeonPrefab.GetComponent<Dungeon>();

        var playerObj = GameObject.Instantiate(Main.Resource.GetObject("Player"));
        Player = playerObj.GetComponent<Player>();
        Player.Initialize();
    }

    /// <summary>
    /// 몬스터 소환 실험용
    /// </summary>
    public void SpawnEnemy()
    {
        var enemyObj1 = GameObject.Instantiate(Main.Resource.GetObject("EnemyMelee"));
        enemyObj1.transform.localPosition = Vector3.zero;

        var enemyObj2 = GameObject.Instantiate(Main.Resource.GetObject("EnemyRange"));
        enemyObj2.transform.localPosition = Vector3.zero;
    }

    #endregion
}
