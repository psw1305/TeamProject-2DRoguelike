using UnityEngine;

public class GameScene : MonoBehaviour
{
    #region Init

    private void Start()
    {
        Main.Resource.Initialize();

        // #1. 플레이어 생성
        Main.Game.SpawnPlayer(this.transform);

        // #2. 게임 레벨 생성
        Main.Game.DungeonGenerate();
    }

    #endregion
}
