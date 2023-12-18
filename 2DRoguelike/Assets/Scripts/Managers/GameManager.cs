
using UnityEngine;

public class GameManager
{
    #region Properties

    public Player Player { get; private set; }

    #endregion

    #region Init

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
