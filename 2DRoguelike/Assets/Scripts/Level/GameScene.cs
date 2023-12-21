using UnityEngine;

public class GameScene : MonoBehaviour
{
    #region Field

    [SerializeField] private Transform inGameCanvas;

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        // #1. 리소스 초기화
        Main.Resource.Initialize();

        // #2. 오브젝트 생성 & 초기화
        Main.Reward.Initialize();
        Main.UI.Initialize(inGameCanvas);
        Main.Game.Initialize();

        // #3. 게임 던전 생성 & 설정
        Main.Game.Dungeon.CreateDungeon();

        // #4. 미니맵 생성
        Main.UI.Minimap.CreatMinimap();

        // #5. NevMesh 영역생성
        Main.Resource.InstantiatePrefab("NevMesh");
    }

    private void Update()
    {
        Main.Game.CurrentState?.UpdateState();
    }

    #endregion
}
