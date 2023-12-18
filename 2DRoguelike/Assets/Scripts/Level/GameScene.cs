using UnityEngine;
using UnityEngine.AI;

public class GameScene : MonoBehaviour
{
    public GameObject nevMesh;

    #region Init

    private void Start()
    {
        Main.Resource.Initialize();

        // #1. 플레이어 생성
        Main.Game.SpawnPlayer(this.transform);

        // #2. 게임 레벨 생성
        Main.Game.DungeonGenerate();

        // #3. 미니맵 생성
        Main.UI.MinimapGenerate();

        // #4. NevMesh 영역생성
        Instantiate(nevMesh);

        // 테스트 적 스폰
        Main.Game.SpawnEnemy();
    }

    #endregion
}
