using UnityEngine;

public class GameManager
{
    #region Properties

    public IGameState CurrentState { get; private set; }
    public PlayingState Playing { get; private set; }
    public PausedState Paused { get; private set; }
    public StopState Stop { get; private set; }
    public ClearedState Clear { get; private set; }

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

        Playing = new PlayingState(this);
        Paused = new PausedState(this);
        Stop = new StopState(this);
        Clear = new ClearedState(this);

        ChangeState(Playing);
    }

    #endregion

    #region Progress

    /// <summary>
    /// 게임 상태 변환 => 일시정지, 플레이
    /// </summary>
    /// <param name="newState">새로 주어질 상태</param>
    public void ChangeState(IGameState newState)
    {
        CurrentState?.ExitState();
        CurrentState = newState;
        CurrentState?.EnterState();
    }

    /// <summary>
    /// 게임 정지 => 플레이어 사망
    /// </summary>
    public void GameStop()
    {
        ChangeState(Stop);
    }

    /// <summary>
    /// 게임 클리어 => 보스 격파
    /// </summary>
    public void GameWin()
    {
        ChangeState(Clear);
    }

    #endregion
}
