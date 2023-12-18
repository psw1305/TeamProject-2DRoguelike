
using UnityEngine;

public class GameManager
{
    #region Properties

    public Dungeon Dungeon { get; private set; }
    public Player Player { get; private set; }

    #endregion

    #region Init

    /// <summary>
    /// 게임 던전 생성
    /// </summary>
    public void DungeonGenerate()
    {
        var dungeonPrefab = GameObject.Instantiate(Main.Resource.GetObject("Dungeon"));
        Dungeon = dungeonPrefab.GetComponent<Dungeon>();
        Dungeon.Initialize();
    }

    /// <summary>
    /// 플레이어 게임 씬 생성 & 초기화
    /// </summary>
    /// <param name="spawn">생성 위치</param>
    public void SpawnPlayer(Transform spawn)
    {
        var playerObj = GameObject.Instantiate(Main.Resource.GetObject("Player"), spawn);
        Player = playerObj.GetComponent<Player>();
    }

    #endregion
}
