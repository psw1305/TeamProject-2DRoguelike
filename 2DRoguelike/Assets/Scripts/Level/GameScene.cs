using UnityEngine;
using UnityEngine.AI;

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

        // #3. 게임 던전 생성 & 설정
        Main.Game.Dungeon.CreateDungeon();

        // #4. 미니맵 생성
        Main.UI.Minimap.CreatMinimap();

        // #5. NevMesh 영역생성
        Main.Resource.InstantiatePrefab("NevMesh");

        // 테스트 적 스폰
        Main.Game.SpawnEnemy();
    }

    #endregion
}
