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

    #endregion
}
