using UnityEngine;

public class GameScene : MonoBehaviour
{
    #region Init

    private void Start()
    {
        Main.Resource.Initialize();

        // #1. 게임 레벨 생성
        DungeonGenerate();

        // #2. 플레이어 생성
        Main.Game.SpawnPlayer(this.transform);
    }

    private void DungeonGenerate()
    {
        var dungeonPrefab = Instantiate(Main.Resource.GetObject("Dungeon"));
        var dungeon = dungeonPrefab.GetComponent<Dungeon>();
        dungeon.Initialize();
    }

    #endregion
}
