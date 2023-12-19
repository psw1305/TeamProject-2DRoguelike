using UnityEngine;

public class GameScene : MonoBehaviour
{
    #region Init

    private void Start()
    {
        // #1. 리소스 초기화
        Main.Resource.Initialize();

        // #2. 오브젝트 생성 & 초기화
        Main.UI.Initialize();
        Main.Game.Initialize();

        // #2. 게임 던전 생성 & 설정
        Main.Game.Dungeon.CreateDungeon();

        // #3. 미니맵 생성
        Main.UI.Minimap.CreatMinimap();
    }

    #endregion
}
